using UGB.Paysa.Wallet.Chambres;
using UGB.Paysa.Wallet.Operations;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using UGB.Paysa.UGB.Paysa.Chambres.Exporting;
using UGB.Paysa.UGB.Paysa.Chambres.Dtos;
using UGB.Paysa.Dto;
using Abp.Application.Services.Dto;
using UGB.Paysa.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using UGB.Paysa.Storage;

namespace UGB.Paysa.UGB.Paysa.Chambres
{
    [AbpAuthorize(AppPermissions.Pages_PaiementLoyers)]
    public class PaiementLoyersAppService : PaysaAppServiceBase, IPaiementLoyersAppService
    {
        private readonly IRepository<PaiementLoyer, string> _paiementLoyerRepository;
        private readonly IPaiementLoyersExcelExporter _paiementLoyersExcelExporter;
        private readonly IRepository<Chambre, string> _lookup_chambreRepository;
        private readonly IRepository<Operation, string> _lookup_operationRepository;

        public PaiementLoyersAppService(IRepository<PaiementLoyer, string> paiementLoyerRepository, IPaiementLoyersExcelExporter paiementLoyersExcelExporter, IRepository<Chambre, string> lookup_chambreRepository, IRepository<Operation, string> lookup_operationRepository)
        {
            _paiementLoyerRepository = paiementLoyerRepository;
            _paiementLoyersExcelExporter = paiementLoyersExcelExporter;
            _lookup_chambreRepository = lookup_chambreRepository;
            _lookup_operationRepository = lookup_operationRepository;

        }

        public async Task<PagedResultDto<GetPaiementLoyerForViewDto>> GetAll(GetAllPaiementLoyersInput input)
        {

            var filteredPaiementLoyers = _paiementLoyerRepository.GetAll()
                        .Include(e => e.ChambreFk)
                        .Include(e => e.OperationFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Mois.Contains(input.Filter) || e.CodePaiement.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MoisFilter), e => e.Mois == input.MoisFilter)
                        .WhereIf(input.MinAnneeFilter != null, e => e.Annee >= input.MinAnneeFilter)
                        .WhereIf(input.MaxAnneeFilter != null, e => e.Annee <= input.MaxAnneeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CodePaiementFilter), e => e.CodePaiement == input.CodePaiementFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ChambreReferenceFilter), e => e.ChambreFk != null && e.ChambreFk.Reference == input.ChambreReferenceFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OperationCodeOperationFilter), e => e.OperationFk != null && e.OperationFk.CodeOperation == input.OperationCodeOperationFilter);

            var pagedAndFilteredPaiementLoyers = filteredPaiementLoyers
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var paiementLoyers = from o in pagedAndFilteredPaiementLoyers
                                 join o1 in _lookup_chambreRepository.GetAll() on o.ChambreId equals o1.Id into j1
                                 from s1 in j1.DefaultIfEmpty()

                                 join o2 in _lookup_operationRepository.GetAll() on o.OperationId equals o2.Id into j2
                                 from s2 in j2.DefaultIfEmpty()

                                 select new
                                 {

                                     o.Mois,
                                     o.Annee,
                                     o.CodePaiement,
                                     Id = o.Id,
                                     ChambreReference = s1 == null || s1.Reference == null ? "" : s1.Reference.ToString(),
                                     OperationCodeOperation = s2 == null || s2.CodeOperation == null ? "" : s2.CodeOperation.ToString(),
                                     OperationDateOperation =  s2.DateOperation
                                 };

            var totalCount = await filteredPaiementLoyers.CountAsync();

            var dbList = await paiementLoyers.ToListAsync();
            var results = new List<GetPaiementLoyerForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetPaiementLoyerForViewDto()
                {
                    PaiementLoyer = new PaiementLoyerDto
                    {

                        Mois = o.Mois,
                        Annee = o.Annee,
                        CodePaiement = o.CodePaiement,
                        Id = o.Id,
                    },
                    ChambreReference = o.ChambreReference,
                    OperationCodeOperation = o.OperationCodeOperation,
                    OperationDateOperation = o.OperationDateOperation,
                };

                results.Add(res);
            }

            return new PagedResultDto<GetPaiementLoyerForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetPaiementLoyerForViewDto> GetPaiementLoyerForView(string id)
        {
            var paiementLoyer = await _paiementLoyerRepository.GetAsync(id);

            var output = new GetPaiementLoyerForViewDto { PaiementLoyer = ObjectMapper.Map<PaiementLoyerDto>(paiementLoyer) };

            if (output.PaiementLoyer.ChambreId != null)
            {
                var _lookupChambre = await _lookup_chambreRepository.FirstOrDefaultAsync((string)output.PaiementLoyer.ChambreId);
                output.ChambreReference = _lookupChambre?.Reference?.ToString();
            }

            if (output.PaiementLoyer.OperationId != null)
            {
                var _lookupOperation = await _lookup_operationRepository.FirstOrDefaultAsync((string)output.PaiementLoyer.OperationId);
                output.OperationCodeOperation = _lookupOperation?.CodeOperation?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_PaiementLoyers_Edit)]
        public async Task<GetPaiementLoyerForEditOutput> GetPaiementLoyerForEdit(EntityDto<string> input)
        {
            var paiementLoyer = await _paiementLoyerRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetPaiementLoyerForEditOutput { PaiementLoyer = ObjectMapper.Map<CreateOrEditPaiementLoyerDto>(paiementLoyer) };

            if (output.PaiementLoyer.ChambreId != null)
            {
                var _lookupChambre = await _lookup_chambreRepository.FirstOrDefaultAsync((string)output.PaiementLoyer.ChambreId);
                output.ChambreReference = _lookupChambre?.Reference?.ToString();
            }

            if (output.PaiementLoyer.OperationId != null)
            {
                var _lookupOperation = await _lookup_operationRepository.FirstOrDefaultAsync((string)output.PaiementLoyer.OperationId);
                output.OperationCodeOperation = _lookupOperation?.CodeOperation?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditPaiementLoyerDto input)
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

        [AbpAuthorize(AppPermissions.Pages_PaiementLoyers_Create)]
        protected virtual async Task Create(CreateOrEditPaiementLoyerDto input)
        {
            var paiementLoyer = ObjectMapper.Map<PaiementLoyer>(input);

            if (AbpSession.TenantId != null)
            {
                paiementLoyer.TenantId = (int?)AbpSession.TenantId;
            }

            if (paiementLoyer.Id.IsNullOrWhiteSpace())
            {
                paiementLoyer.Id = Guid.NewGuid().ToString();
            }

            await _paiementLoyerRepository.InsertAsync(paiementLoyer);

        }

        [AbpAuthorize(AppPermissions.Pages_PaiementLoyers_Edit)]
        protected virtual async Task Update(CreateOrEditPaiementLoyerDto input)
        {
            var paiementLoyer = await _paiementLoyerRepository.FirstOrDefaultAsync((string)input.Id);
            ObjectMapper.Map(input, paiementLoyer);

        }

        [AbpAuthorize(AppPermissions.Pages_PaiementLoyers_Delete)]
        public async Task Delete(EntityDto<string> input)
        {
            await _paiementLoyerRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetPaiementLoyersToExcel(GetAllPaiementLoyersForExcelInput input)
        {

            var filteredPaiementLoyers = _paiementLoyerRepository.GetAll()
                        .Include(e => e.ChambreFk)
                        .Include(e => e.OperationFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Mois.Contains(input.Filter) || e.CodePaiement.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MoisFilter), e => e.Mois == input.MoisFilter)
                        .WhereIf(input.MinAnneeFilter != null, e => e.Annee >= input.MinAnneeFilter)
                        .WhereIf(input.MaxAnneeFilter != null, e => e.Annee <= input.MaxAnneeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CodePaiementFilter), e => e.CodePaiement == input.CodePaiementFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ChambreReferenceFilter), e => e.ChambreFk != null && e.ChambreFk.Reference == input.ChambreReferenceFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OperationCodeOperationFilter), e => e.OperationFk != null && e.OperationFk.CodeOperation == input.OperationCodeOperationFilter);

            var query = (from o in filteredPaiementLoyers
                         join o1 in _lookup_chambreRepository.GetAll() on o.ChambreId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_operationRepository.GetAll() on o.OperationId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         select new GetPaiementLoyerForViewDto()
                         {
                             PaiementLoyer = new PaiementLoyerDto
                             {
                                 Mois = o.Mois,
                                 Annee = o.Annee,
                                 CodePaiement = o.CodePaiement,
                                 Id = o.Id
                             },
                             ChambreReference = s1 == null || s1.Reference == null ? "" : s1.Reference.ToString(),
                             OperationCodeOperation = s2 == null || s2.CodeOperation == null ? "" : s2.CodeOperation.ToString()
                         });

            var paiementLoyerListDtos = await query.ToListAsync();

            return _paiementLoyersExcelExporter.ExportToFile(paiementLoyerListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_PaiementLoyers)]
        public async Task<PagedResultDto<PaiementLoyerChambreLookupTableDto>> GetAllChambreForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_chambreRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.Reference != null && e.Reference.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var chambreList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<PaiementLoyerChambreLookupTableDto>();
            foreach (var chambre in chambreList)
            {
                lookupTableDtoList.Add(new PaiementLoyerChambreLookupTableDto
                {
                    Id = chambre.Id,
                    DisplayName = chambre.Reference?.ToString()
                });
            }

            return new PagedResultDto<PaiementLoyerChambreLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
        [AbpAuthorize(AppPermissions.Pages_PaiementLoyers)]
        public async Task<List<PaiementLoyerOperationLookupTableDto>> GetAllOperationForTableDropdown()
        {
            return await _lookup_operationRepository.GetAll()
                .Select(operation => new PaiementLoyerOperationLookupTableDto
                {
                    Id = operation.Id,
                    DisplayName = operation == null || operation.CodeOperation == null ? "" : operation.CodeOperation.ToString()
                }).ToListAsync();
        }

    }
}