using UGB.Paysa.Wallet.Etudiants;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using UGB.Paysa.Wallet.Comptes.Exporting;
using UGB.Paysa.Wallet.Comptes.Dtos;
using UGB.Paysa.Dto;
using Abp.Application.Services.Dto;
using UGB.Paysa.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using UGB.Paysa.Storage;

namespace UGB.Paysa.Wallet.Comptes
{
    [AbpAuthorize(AppPermissions.Pages_Comptes)]
    public class ComptesAppService : PaysaAppServiceBase, IComptesAppService
    {
        private readonly IRepository<Compte, string> _compteRepository;
        private readonly IComptesExcelExporter _comptesExcelExporter;
        private readonly IRepository<Etudiant, string> _lookup_etudiantRepository;

        public ComptesAppService(IRepository<Compte, string> compteRepository, IComptesExcelExporter comptesExcelExporter, IRepository<Etudiant, string> lookup_etudiantRepository)
        {
            _compteRepository = compteRepository;
            _comptesExcelExporter = comptesExcelExporter;
            _lookup_etudiantRepository = lookup_etudiantRepository;

        }

        public async Task<PagedResultDto<GetCompteForViewDto>> GetAll(GetAllComptesInput input)
        {

            var filteredComptes = _compteRepository.GetAll()
                        .Include(e => e.EtudiantFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.NumeroCompte.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NumeroCompteFilter), e => e.NumeroCompte == input.NumeroCompteFilter)
                        .WhereIf(input.MinSoldeFilter != null, e => e.Solde >= input.MinSoldeFilter)
                        .WhereIf(input.MaxSoldeFilter != null, e => e.Solde <= input.MaxSoldeFilter)
                        .WhereIf(input.IsActiveFilter.HasValue && input.IsActiveFilter > -1, e => (input.IsActiveFilter == 1 && e.IsActive) || (input.IsActiveFilter == 0 && !e.IsActive))
                        .WhereIf(input.MinDateCreationFilter != null, e => e.DateCreation >= input.MinDateCreationFilter)
                        .WhereIf(input.MaxDateCreationFilter != null, e => e.DateCreation <= input.MaxDateCreationFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EtudiantCodeEtudiantFilter), e => e.EtudiantFk != null && e.EtudiantFk.CodeEtudiant == input.EtudiantCodeEtudiantFilter);

            var pagedAndFilteredComptes = filteredComptes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var comptes = from o in pagedAndFilteredComptes
                          join o1 in _lookup_etudiantRepository.GetAll() on o.EtudiantId equals o1.Id into j1
                          from s1 in j1.DefaultIfEmpty()

                          select new
                          {

                              o.NumeroCompte,
                              o.Solde,
                              o.IsActive,
                              o.DateCreation,
                              Id = o.Id,
                              EtudiantCodeEtudiant = s1 == null || s1.CodeEtudiant == null ? "" : s1.CodeEtudiant.ToString()
                          };

            var totalCount = await filteredComptes.CountAsync();

            var dbList = await comptes.ToListAsync();
            var results = new List<GetCompteForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetCompteForViewDto()
                {
                    Compte = new CompteDto
                    {

                        NumeroCompte = o.NumeroCompte,
                        Solde = o.Solde,
                        IsActive = o.IsActive,
                        DateCreation = o.DateCreation,
                        Id = o.Id,
                    },
                    EtudiantCodeEtudiant = o.EtudiantCodeEtudiant
                };

                results.Add(res);
            }

            return new PagedResultDto<GetCompteForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetCompteForViewDto> GetCompteForView(string id)
        {
            var compte = await _compteRepository.GetAsync(id);

            var output = new GetCompteForViewDto { Compte = ObjectMapper.Map<CompteDto>(compte) };

            if (output.Compte.EtudiantId != null)
            {
                var _lookupEtudiant = await _lookup_etudiantRepository.FirstOrDefaultAsync((string)output.Compte.EtudiantId);
                output.EtudiantCodeEtudiant = _lookupEtudiant?.CodeEtudiant?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Comptes_Edit)]
        public async Task<GetCompteForEditOutput> GetCompteForEdit(EntityDto<string> input)
        {
            var compte = await _compteRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetCompteForEditOutput { Compte = ObjectMapper.Map<CreateOrEditCompteDto>(compte) };

            if (output.Compte.EtudiantId != null)
            {
                var _lookupEtudiant = await _lookup_etudiantRepository.FirstOrDefaultAsync((string)output.Compte.EtudiantId);
                output.EtudiantCodeEtudiant = _lookupEtudiant?.CodeEtudiant?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditCompteDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Comptes_Create)]
        protected virtual async Task Create(CreateOrEditCompteDto input)
        {
            var compte = ObjectMapper.Map<Compte>(input);

            if (AbpSession.TenantId != null)
            {
                compte.TenantId = (int?)AbpSession.TenantId;
            }

            if (compte.Id.IsNullOrWhiteSpace())
            {
                compte.Id = Guid.NewGuid().ToString();
            }

            await _compteRepository.InsertAsync(compte);

        }

        [AbpAuthorize(AppPermissions.Pages_Comptes_Edit)]
        protected virtual async Task Update(CreateOrEditCompteDto input)
        {
            var compte = await _compteRepository.FirstOrDefaultAsync((string)input.Id);
            ObjectMapper.Map(input, compte);

        }

        [AbpAuthorize(AppPermissions.Pages_Comptes_Delete)]
        public async Task Delete(EntityDto<string> input)
        {
            await _compteRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetComptesToExcel(GetAllComptesForExcelInput input)
        {

            var filteredComptes = _compteRepository.GetAll()
                        .Include(e => e.EtudiantFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.NumeroCompte.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NumeroCompteFilter), e => e.NumeroCompte == input.NumeroCompteFilter)
                        .WhereIf(input.MinSoldeFilter != null, e => e.Solde >= input.MinSoldeFilter)
                        .WhereIf(input.MaxSoldeFilter != null, e => e.Solde <= input.MaxSoldeFilter)
                        .WhereIf(input.IsActiveFilter.HasValue && input.IsActiveFilter > -1, e => (input.IsActiveFilter == 1 && e.IsActive) || (input.IsActiveFilter == 0 && !e.IsActive))
                        .WhereIf(input.MinDateCreationFilter != null, e => e.DateCreation >= input.MinDateCreationFilter)
                        .WhereIf(input.MaxDateCreationFilter != null, e => e.DateCreation <= input.MaxDateCreationFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EtudiantCodeEtudiantFilter), e => e.EtudiantFk != null && e.EtudiantFk.CodeEtudiant == input.EtudiantCodeEtudiantFilter);

            var query = (from o in filteredComptes
                         join o1 in _lookup_etudiantRepository.GetAll() on o.EtudiantId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         select new GetCompteForViewDto()
                         {
                             Compte = new CompteDto
                             {
                                 NumeroCompte = o.NumeroCompte,
                                 Solde = o.Solde,
                                 IsActive = o.IsActive,
                                 DateCreation = o.DateCreation,
                                 Id = o.Id
                             },
                             EtudiantCodeEtudiant = s1 == null || s1.CodeEtudiant == null ? "" : s1.CodeEtudiant.ToString()
                         });

            var compteListDtos = await query.ToListAsync();

            return _comptesExcelExporter.ExportToFile(compteListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Comptes)]
        public async Task<List<CompteEtudiantLookupTableDto>> GetAllEtudiantForTableDropdown()
        {
            return await _lookup_etudiantRepository.GetAll()
                .Select(etudiant => new CompteEtudiantLookupTableDto
                {
                    Id = etudiant.Id,
                    DisplayName = etudiant == null || etudiant.CodeEtudiant == null ? "" : etudiant.CodeEtudiant.ToString()
                }).ToListAsync();
        }

    }
}