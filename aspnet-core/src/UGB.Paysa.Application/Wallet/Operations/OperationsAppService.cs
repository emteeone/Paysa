using UGB.Paysa.Wallet.Comptes;
using UGB.Paysa.Wallet.Operations;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using UGB.Paysa.Wallet.Operations.Exporting;
using UGB.Paysa.Wallet.Operations.Dtos;
using UGB.Paysa.Dto;
using Abp.Application.Services.Dto;
using UGB.Paysa.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using UGB.Paysa.Storage;

namespace UGB.Paysa.Wallet.Operations
{
    [AbpAuthorize(AppPermissions.Pages_Operations)]
    public class OperationsAppService : PaysaAppServiceBase, IOperationsAppService
    {
        private readonly IRepository<Operation, string> _operationRepository;
        private readonly IOperationsExcelExporter _operationsExcelExporter;
        private readonly IRepository<Compte, string> _lookup_compteRepository;
        private readonly IRepository<TypeOperation, string> _lookup_typeOperationRepository;
        private readonly IComptesAppService _compteAppService;
         

        public OperationsAppService(IRepository<Operation, string> operationRepository, 
            IOperationsExcelExporter operationsExcelExporter, 
            IRepository<Compte, string> lookup_compteRepository, 
            IRepository<TypeOperation, string> lookup_typeOperationRepository, IComptesAppService comptesAppService)
        {
            _operationRepository = operationRepository;
            _operationsExcelExporter = operationsExcelExporter;
            _lookup_compteRepository = lookup_compteRepository;
            _lookup_typeOperationRepository = lookup_typeOperationRepository;
            _compteAppService = comptesAppService;

        }

        public async Task<PagedResultDto<GetOperationForViewDto>> GetAll(GetAllOperationsInput input)
        {

            var filteredOperations = _operationRepository.GetAll()
                        .Include(e => e.CompteFk)
                        .Include(e => e.TypeProductionFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.CodeOperation.Contains(input.Filter) || e.Discriminator.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CodeOperationFilter), e => e.CodeOperation == input.CodeOperationFilter)
                        .WhereIf(input.MinDateOperationFilter != null, e => e.DateOperation >= input.MinDateOperationFilter)
                        .WhereIf(input.MaxDateOperationFilter != null, e => e.DateOperation <= input.MaxDateOperationFilter)
                        .WhereIf(input.MinMontantFilter != null, e => e.Montant >= input.MinMontantFilter)
                        .WhereIf(input.MaxMontantFilter != null, e => e.Montant <= input.MaxMontantFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DiscriminatorFilter), e => e.Discriminator == input.DiscriminatorFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CompteNumeroCompteFilter), e => e.CompteFk != null && e.CompteFk.NumeroCompte == input.CompteNumeroCompteFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TypeOperationNomFilter), e => e.TypeProductionFk != null && e.TypeProductionFk.Reference == input.TypeOperationNomFilter);

            var pagedAndFilteredOperations = filteredOperations
                .OrderBy(input.Sorting ?? "dateOperation desc")
                .PageBy(input);

            var operations = from o in pagedAndFilteredOperations
                             join o1 in _lookup_compteRepository.GetAll() on o.CompteId equals o1.Id into j1
                             from s1 in j1.DefaultIfEmpty()

                             join o2 in _lookup_typeOperationRepository.GetAll() on o.TypeProductionId equals o2.Id into j2
                             from s2 in j2.DefaultIfEmpty()

                             select new
                             {

                                 o.CodeOperation,
                                 o.DateOperation,
                                 o.Montant,
                                 o.Discriminator,
                                 Id = o.Id,
                                 CompteNumeroCompte = s1 == null || s1.NumeroCompte == null ? "" : s1.NumeroCompte.ToString(),
                                 TypeOperationNom = s2 == null || s2.Nom == null ? "" : s2.Nom.ToString()
                             };

            var totalCount = await filteredOperations.CountAsync();

            var dbList = await operations.ToListAsync();
            var results = new List<GetOperationForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetOperationForViewDto()
                {
                    Operation = new OperationDto
                    {

                        CodeOperation = o.CodeOperation,
                        DateOperation = o.DateOperation,
                        Montant = o.Montant,
                        Discriminator = o.Discriminator,
                        Id = o.Id,
                    },
                    CompteNumeroCompte = o.CompteNumeroCompte,
                    TypeOperationNom = o.TypeOperationNom
                };

                results.Add(res);
            }

            return new PagedResultDto<GetOperationForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetOperationForViewDto> GetOperationForView(string id)
        {
            var operation = await _operationRepository.GetAsync(id);

            var output = new GetOperationForViewDto { Operation = ObjectMapper.Map<OperationDto>(operation) };

            if (output.Operation.CompteId != null)
            {
                var _lookupCompte = await _lookup_compteRepository.FirstOrDefaultAsync((string)output.Operation.CompteId);
                output.CompteNumeroCompte = _lookupCompte?.NumeroCompte?.ToString();
            }

            if (output.Operation.TypeProductionId != null)
            {
                var _lookupTypeOperation = await _lookup_typeOperationRepository.FirstOrDefaultAsync((string)output.Operation.TypeProductionId);
                output.TypeOperationNom = _lookupTypeOperation?.Nom?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Operations_Edit)]
        public async Task<GetOperationForEditOutput> GetOperationForEdit(EntityDto<string> input)
        {
            var operation = await _operationRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetOperationForEditOutput { Operation = ObjectMapper.Map<CreateOrEditOperationDto>(operation) };

            if (output.Operation.CompteId != null)
            {
                var _lookupCompte = await _lookup_compteRepository.FirstOrDefaultAsync((string)output.Operation.CompteId);
                output.CompteNumeroCompte = _lookupCompte?.NumeroCompte?.ToString();
            }

            if (output.Operation.TypeProductionId != null)
            {
                var _lookupTypeOperation = await _lookup_typeOperationRepository.FirstOrDefaultAsync((string)output.Operation.TypeProductionId);
                output.TypeOperationNom = _lookupTypeOperation?.Nom?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditOperationDto input)
        {
            if (input.Id.IsNullOrWhiteSpace())
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Operations_Create)]
        protected virtual async Task Create(CreateOrEditOperationDto input)
        {
            var operation = ObjectMapper.Map<Operation>(input);
            operation.DateOperation = DateTime.Now;
            if (AbpSession.TenantId != null)
            {
                operation.TenantId = (int?)AbpSession.TenantId;
            }

            if (operation.Id.IsNullOrWhiteSpace())
            {
                operation.Id = Guid.NewGuid().ToString();
            }

            if (input.Discriminator == "Debit")
            {
                var result = await _compteAppService.DebiterCompte(new Comptes.Dtos.EditSoldeCompteDto { Montant = input.Montant, Id = input.CompteId }); ;
            }
            else if (input.Discriminator == "Credit")
            {
                operation.Montant = input.Montant;
                var result = await _compteAppService.CrediterCompte(new Comptes.Dtos.EditSoldeCompteDto { Montant = input.Montant, Id = input.CompteId }); ;
            }

            await _operationRepository.InsertAsync(operation);
            
        }

        [AbpAuthorize(AppPermissions.Pages_Operations_Edit)]
        protected virtual async Task Update(CreateOrEditOperationDto input)
        {
            var operation = await _operationRepository.FirstOrDefaultAsync((string)input.Id);
            ObjectMapper.Map(input, operation);

        }

        [AbpAuthorize(AppPermissions.Pages_Operations_Delete)]
        public async Task Delete(EntityDto<string> input)
        {
            await _operationRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetOperationsToExcel(GetAllOperationsForExcelInput input)
        {

            var filteredOperations = _operationRepository.GetAll()
                        .Include(e => e.CompteFk)
                        .Include(e => e.TypeProductionFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.CodeOperation.Contains(input.Filter) || e.Discriminator.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CodeOperationFilter), e => e.CodeOperation == input.CodeOperationFilter)
                        .WhereIf(input.MinDateOperationFilter != null, e => e.DateOperation >= input.MinDateOperationFilter)
                        .WhereIf(input.MaxDateOperationFilter != null, e => e.DateOperation <= input.MaxDateOperationFilter)
                        .WhereIf(input.MinMontantFilter != null, e => e.Montant >= input.MinMontantFilter)
                        .WhereIf(input.MaxMontantFilter != null, e => e.Montant <= input.MaxMontantFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DiscriminatorFilter), e => e.Discriminator == input.DiscriminatorFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CompteNumeroCompteFilter), e => e.CompteFk != null && e.CompteFk.NumeroCompte == input.CompteNumeroCompteFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TypeOperationNomFilter), e => e.TypeProductionFk != null && e.TypeProductionFk.Nom == input.TypeOperationNomFilter);

            var query = (from o in filteredOperations
                         join o1 in _lookup_compteRepository.GetAll() on o.CompteId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_typeOperationRepository.GetAll() on o.TypeProductionId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         select new GetOperationForViewDto()
                         {
                             Operation = new OperationDto
                             {
                                 CodeOperation = o.CodeOperation,
                                 DateOperation = o.DateOperation,
                                 Montant = o.Montant,
                                 Discriminator = o.Discriminator,
                                 Id = o.Id
                             },
                             CompteNumeroCompte = s1 == null || s1.NumeroCompte == null ? "" : s1.NumeroCompte.ToString(),
                             TypeOperationNom = s2 == null || s2.Nom == null ? "" : s2.Nom.ToString()
                         });

            var operationListDtos = await query.ToListAsync();

            return _operationsExcelExporter.ExportToFile(operationListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Operations)]
        public async Task<PagedResultDto<OperationCompteLookupTableDto>> GetAllCompteForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_compteRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.NumeroCompte != null && e.NumeroCompte.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var compteList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<OperationCompteLookupTableDto>();
            foreach (var compte in compteList)
            {
                lookupTableDtoList.Add(new OperationCompteLookupTableDto
                {
                    Id = compte.Id,
                    DisplayName = compte.NumeroCompte?.ToString()
                });
            }

            return new PagedResultDto<OperationCompteLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
        [AbpAuthorize(AppPermissions.Pages_Operations)]
        public async Task<List<OperationTypeOperationLookupTableDto>> GetAllTypeOperationForTableDropdown()
        {
            return await _lookup_typeOperationRepository.GetAll()
                .Select(typeOperation => new OperationTypeOperationLookupTableDto
                {
                    Id = typeOperation.Id,
                    DisplayName = typeOperation == null || typeOperation.Nom == null ? "" : typeOperation.Nom.ToString()
                }).ToListAsync();
        }

        public async Task<string> GenerateCodeOperation()
        {
            //var operation = await _operationRepository.GetAll().OrderByDescending(operation => operation.DateOperation).LastAsync() ;

            //var reference = operation.CodeOperation.Split("-").Last()

            return Guid.NewGuid().ToString(); ;
        }
    }
}