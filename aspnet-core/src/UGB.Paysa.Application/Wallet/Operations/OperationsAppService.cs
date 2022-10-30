using UGB.Paysa.Wallet.Comptes;

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

        public OperationsAppService(IRepository<Operation, string> operationRepository, IOperationsExcelExporter operationsExcelExporter, IRepository<Compte, string> lookup_compteRepository)
        {
            _operationRepository = operationRepository;
            _operationsExcelExporter = operationsExcelExporter;
            _lookup_compteRepository = lookup_compteRepository;

        }
        [AbpAllowAnonymous]
        public async Task<PagedResultDto<GetOperationForViewDto>> GetAll(GetAllOperationsInput input)
        {

            var filteredOperations = _operationRepository.GetAll()
                        .Include(e => e.CompteFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.CodeOperation.Contains(input.Filter) || e.Discriminator.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CodeOperationFilter), e => e.CodeOperation == input.CodeOperationFilter)
                        .WhereIf(input.MinDateOperationFilter != null, e => e.DateOperation >= input.MinDateOperationFilter)
                        .WhereIf(input.MaxDateOperationFilter != null, e => e.DateOperation <= input.MaxDateOperationFilter)
                        .WhereIf(input.MinMontantFilter != null, e => e.Montant >= input.MinMontantFilter)
                        .WhereIf(input.MaxMontantFilter != null, e => e.Montant <= input.MaxMontantFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DiscriminatorFilter), e => e.Discriminator == input.DiscriminatorFilter)
                        .WhereIf(input.MinCreationTimeFilter != null, e => e.CreationTime >= input.MinCreationTimeFilter)
                        .WhereIf(input.MaxCreationTimeFilter != null, e => e.CreationTime <= input.MaxCreationTimeFilter)
                        .WhereIf(input.MinLastModificationTimeFilter != null, e => e.LastModificationTime >= input.MinLastModificationTimeFilter)
                        .WhereIf(input.MaxLastModificationTimeFilter != null, e => e.LastModificationTime <= input.MaxLastModificationTimeFilter)
                        .WhereIf(input.IsDeletedFilter.HasValue && input.IsDeletedFilter > -1, e => (input.IsDeletedFilter == 1 && e.IsDeleted) || (input.IsDeletedFilter == 0 && !e.IsDeleted))
                        .WhereIf(input.MinDeletionTimeFilter != null, e => e.DeletionTime >= input.MinDeletionTimeFilter)
                        .WhereIf(input.MaxDeletionTimeFilter != null, e => e.DeletionTime <= input.MaxDeletionTimeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CompteNumeroCompteFilter), e => e.CompteFk != null && e.CompteFk.NumeroCompte == input.CompteNumeroCompteFilter);

            var pagedAndFilteredOperations = filteredOperations
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var operations = from o in pagedAndFilteredOperations
                             join o1 in _lookup_compteRepository.GetAll() on o.CompteId equals o1.Id into j1
                             from s1 in j1.DefaultIfEmpty()

                             select new
                             {

                                 o.CodeOperation,
                                 o.DateOperation,
                                 o.Montant,
                                 o.Discriminator,
                                 o.CreationTime,
                                 o.LastModificationTime,
                                 o.IsDeleted,
                                 o.DeletionTime,
                                 Id = o.Id,
                                 CompteNumeroCompte = s1 == null || s1.NumeroCompte == null ? "" : s1.NumeroCompte.ToString()
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
                        CreationTime = o.CreationTime,
                        LastModificationTime = o.LastModificationTime,
                        IsDeleted = o.IsDeleted,
                        DeletionTime = o.DeletionTime,
                        Id = o.Id,
                    },
                    CompteNumeroCompte = o.CompteNumeroCompte
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

            if (AbpSession.TenantId != null)
            {
                operation.TenantId = (int?)AbpSession.TenantId;
            }

            if (operation.Id.IsNullOrWhiteSpace())
            {
                operation.Id = Guid.NewGuid().ToString();
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
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.CodeOperation.Contains(input.Filter) || e.Discriminator.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CodeOperationFilter), e => e.CodeOperation == input.CodeOperationFilter)
                        .WhereIf(input.MinDateOperationFilter != null, e => e.DateOperation >= input.MinDateOperationFilter)
                        .WhereIf(input.MaxDateOperationFilter != null, e => e.DateOperation <= input.MaxDateOperationFilter)
                        .WhereIf(input.MinMontantFilter != null, e => e.Montant >= input.MinMontantFilter)
                        .WhereIf(input.MaxMontantFilter != null, e => e.Montant <= input.MaxMontantFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DiscriminatorFilter), e => e.Discriminator == input.DiscriminatorFilter)
                        .WhereIf(input.MinCreationTimeFilter != null, e => e.CreationTime >= input.MinCreationTimeFilter)
                        .WhereIf(input.MaxCreationTimeFilter != null, e => e.CreationTime <= input.MaxCreationTimeFilter)
                        .WhereIf(input.MinLastModificationTimeFilter != null, e => e.LastModificationTime >= input.MinLastModificationTimeFilter)
                        .WhereIf(input.MaxLastModificationTimeFilter != null, e => e.LastModificationTime <= input.MaxLastModificationTimeFilter)
                        .WhereIf(input.IsDeletedFilter.HasValue && input.IsDeletedFilter > -1, e => (input.IsDeletedFilter == 1 && e.IsDeleted) || (input.IsDeletedFilter == 0 && !e.IsDeleted))
                        .WhereIf(input.MinDeletionTimeFilter != null, e => e.DeletionTime >= input.MinDeletionTimeFilter)
                        .WhereIf(input.MaxDeletionTimeFilter != null, e => e.DeletionTime <= input.MaxDeletionTimeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CompteNumeroCompteFilter), e => e.CompteFk != null && e.CompteFk.NumeroCompte == input.CompteNumeroCompteFilter);

            var query = (from o in filteredOperations
                         join o1 in _lookup_compteRepository.GetAll() on o.CompteId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         select new GetOperationForViewDto()
                         {
                             Operation = new OperationDto
                             {
                                 CodeOperation = o.CodeOperation,
                                 DateOperation = o.DateOperation,
                                 Montant = o.Montant,
                                 Discriminator = o.Discriminator,
                                 CreationTime = o.CreationTime,
                                 LastModificationTime = o.LastModificationTime,
                                 IsDeleted = o.IsDeleted,
                                 DeletionTime = o.DeletionTime,
                                 Id = o.Id
                             },
                             CompteNumeroCompte = s1 == null || s1.NumeroCompte == null ? "" : s1.NumeroCompte.ToString()
                         });

            var operationListDtos = await query.ToListAsync();

            return _operationsExcelExporter.ExportToFile(operationListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Operations)]
        public async Task<List<OperationCompteLookupTableDto>> GetAllCompteForTableDropdown()
        {
            return await _lookup_compteRepository.GetAll()
                .Select(compte => new OperationCompteLookupTableDto
                {
                    Id = compte.Id,
                    DisplayName = compte == null || compte.NumeroCompte == null ? "" : compte.NumeroCompte.ToString()
                }).ToListAsync();
        }

    }
}