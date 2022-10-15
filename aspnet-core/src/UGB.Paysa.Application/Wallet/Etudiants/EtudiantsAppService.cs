using UGB.Paysa.Wallet.Chambres;

using UGB.Paysa.Wallet.Enum;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using UGB.Paysa.Wallet.Etudiants.Exporting;
using UGB.Paysa.Wallet.Etudiants.Dtos;
using UGB.Paysa.Dto;
using Abp.Application.Services.Dto;
using UGB.Paysa.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using UGB.Paysa.Storage;

namespace UGB.Paysa.Wallet.Etudiants
{
    [AbpAuthorize(AppPermissions.Pages_Etudiants)]
    public class EtudiantsAppService : PaysaAppServiceBase, IEtudiantsAppService
    {
        private readonly IRepository<Etudiant, string> _etudiantRepository;
        private readonly IEtudiantsExcelExporter _etudiantsExcelExporter;
        private readonly IRepository<Chambre, string> _lookup_chambreRepository;

        public EtudiantsAppService(IRepository<Etudiant, string> etudiantRepository, IEtudiantsExcelExporter etudiantsExcelExporter, IRepository<Chambre, string> lookup_chambreRepository)
        {
            _etudiantRepository = etudiantRepository;
            _etudiantsExcelExporter = etudiantsExcelExporter;
            _lookup_chambreRepository = lookup_chambreRepository;

        }

        public async Task<PagedResultDto<GetEtudiantForViewDto>> GetAll(GetAllEtudiantsInput input)
        {
            var sexeFilter = input.SexeFilter.HasValue
                        ? (Sexe)input.SexeFilter
                        : default;
            var situationMatrimonialeFilter = input.SituationMatrimonialeFilter.HasValue
                ? (SituationMatrimoniale)input.SituationMatrimonialeFilter
                : default;

            var filteredEtudiants = _etudiantRepository.GetAll()
                        .Include(e => e.ChambreFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.INE.Contains(input.Filter) || e.CodeEtudiant.Contains(input.Filter) || e.Prenom.Contains(input.Filter) || e.Nom.Contains(input.Filter) || e.LieuDeNaissance.Contains(input.Filter) || e.CIN.Contains(input.Filter) || e.Adresse.Contains(input.Filter) || e.Ville.Contains(input.Filter) || e.Region.Contains(input.Filter) || e.Pays.Contains(input.Filter) || e.Email.Contains(input.Filter) || e.Telephone.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.INEFilter), e => e.INE == input.INEFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CodeEtudiantFilter), e => e.CodeEtudiant == input.CodeEtudiantFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PrenomFilter), e => e.Prenom == input.PrenomFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NomFilter), e => e.Nom == input.NomFilter)
                        .WhereIf(input.SexeFilter.HasValue && input.SexeFilter > -1, e => e.Sexe == sexeFilter)
                        .WhereIf(input.MinDateDeNaissanceFilter != null, e => e.DateDeNaissance >= input.MinDateDeNaissanceFilter)
                        .WhereIf(input.MaxDateDeNaissanceFilter != null, e => e.DateDeNaissance <= input.MaxDateDeNaissanceFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LieuDeNaissanceFilter), e => e.LieuDeNaissance == input.LieuDeNaissanceFilter)
                        .WhereIf(input.SituationMatrimonialeFilter.HasValue && input.SituationMatrimonialeFilter > -1, e => e.SituationMatrimoniale == situationMatrimonialeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CINFilter), e => e.CIN == input.CINFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AdresseFilter), e => e.Adresse == input.AdresseFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.VilleFilter), e => e.Ville == input.VilleFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RegionFilter), e => e.Region == input.RegionFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PaysFilter), e => e.Pays == input.PaysFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmailFilter), e => e.Email == input.EmailFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TelephoneFilter), e => e.Telephone == input.TelephoneFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ChambreReferenceFilter), e => e.ChambreFk != null && e.ChambreFk.Reference == input.ChambreReferenceFilter);

            var pagedAndFilteredEtudiants = filteredEtudiants
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var etudiants = from o in pagedAndFilteredEtudiants
                            join o1 in _lookup_chambreRepository.GetAll() on o.ChambreId equals o1.Id into j1
                            from s1 in j1.DefaultIfEmpty()

                            select new
                            {

                                o.INE,
                                o.CodeEtudiant,
                                o.Prenom,
                                o.Nom,
                                o.Sexe,
                                o.DateDeNaissance,
                                o.LieuDeNaissance,
                                o.SituationMatrimoniale,
                                o.CIN,
                                o.Adresse,
                                o.Ville,
                                o.Region,
                                o.Pays,
                                o.Email,
                                o.Telephone,
                                Id = o.Id,
                                ChambreReference = s1 == null || s1.Reference == null ? "" : s1.Reference.ToString()
                            };

            var totalCount = await filteredEtudiants.CountAsync();

            var dbList = await etudiants.ToListAsync();
            var results = new List<GetEtudiantForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetEtudiantForViewDto()
                {
                    Etudiant = new EtudiantDto
                    {

                        INE = o.INE,
                        CodeEtudiant = o.CodeEtudiant,
                        Prenom = o.Prenom,
                        Nom = o.Nom,
                        Sexe = o.Sexe,
                        DateDeNaissance = o.DateDeNaissance,
                        LieuDeNaissance = o.LieuDeNaissance,
                        SituationMatrimoniale = o.SituationMatrimoniale,
                        CIN = o.CIN,
                        Adresse = o.Adresse,
                        Ville = o.Ville,
                        Region = o.Region,
                        Pays = o.Pays,
                        Email = o.Email,
                        Telephone = o.Telephone,
                        Id = o.Id,
                    },
                    ChambreReference = o.ChambreReference
                };

                results.Add(res);
            }

            return new PagedResultDto<GetEtudiantForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetEtudiantForViewDto> GetEtudiantForView(string id)
        {
            var etudiant = await _etudiantRepository.GetAsync(id);

            var output = new GetEtudiantForViewDto { Etudiant = ObjectMapper.Map<EtudiantDto>(etudiant) };

            if (output.Etudiant.ChambreId != null)
            {
                var _lookupChambre = await _lookup_chambreRepository.FirstOrDefaultAsync((string)output.Etudiant.ChambreId);
                output.ChambreReference = _lookupChambre?.Reference?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Etudiants_Edit)]
        public async Task<GetEtudiantForEditOutput> GetEtudiantForEdit(EntityDto<string> input)
        {
            var etudiant = await _etudiantRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEtudiantForEditOutput { Etudiant = ObjectMapper.Map<CreateOrEditEtudiantDto>(etudiant) };

            if (output.Etudiant.ChambreId != null)
            {
                var _lookupChambre = await _lookup_chambreRepository.FirstOrDefaultAsync((string)output.Etudiant.ChambreId);
                output.ChambreReference = _lookupChambre?.Reference?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditEtudiantDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Etudiants_Create)]
        protected virtual async Task Create(CreateOrEditEtudiantDto input)
        {
            var etudiant = ObjectMapper.Map<Etudiant>(input);

            if (AbpSession.TenantId != null)
            {
                etudiant.TenantId = (int?)AbpSession.TenantId;
            }

            if (etudiant.Id.IsNullOrWhiteSpace())
            {
                etudiant.Id = Guid.NewGuid().ToString();
            }

            await _etudiantRepository.InsertAsync(etudiant);

        }

        [AbpAuthorize(AppPermissions.Pages_Etudiants_Edit)]
        protected virtual async Task Update(CreateOrEditEtudiantDto input)
        {
            var etudiant = await _etudiantRepository.FirstOrDefaultAsync((string)input.Id);
            ObjectMapper.Map(input, etudiant);

        }

        [AbpAuthorize(AppPermissions.Pages_Etudiants_Delete)]
        public async Task Delete(EntityDto<string> input)
        {
            await _etudiantRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetEtudiantsToExcel(GetAllEtudiantsForExcelInput input)
        {
            var sexeFilter = input.SexeFilter.HasValue
                        ? (Sexe)input.SexeFilter
                        : default;
            var situationMatrimonialeFilter = input.SituationMatrimonialeFilter.HasValue
                ? (SituationMatrimoniale)input.SituationMatrimonialeFilter
                : default;

            var filteredEtudiants = _etudiantRepository.GetAll()
                        .Include(e => e.ChambreFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.INE.Contains(input.Filter) || e.CodeEtudiant.Contains(input.Filter) || e.Prenom.Contains(input.Filter) || e.Nom.Contains(input.Filter) || e.LieuDeNaissance.Contains(input.Filter) || e.CIN.Contains(input.Filter) || e.Adresse.Contains(input.Filter) || e.Ville.Contains(input.Filter) || e.Region.Contains(input.Filter) || e.Pays.Contains(input.Filter) || e.Email.Contains(input.Filter) || e.Telephone.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.INEFilter), e => e.INE == input.INEFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CodeEtudiantFilter), e => e.CodeEtudiant == input.CodeEtudiantFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PrenomFilter), e => e.Prenom == input.PrenomFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NomFilter), e => e.Nom == input.NomFilter)
                        .WhereIf(input.SexeFilter.HasValue && input.SexeFilter > -1, e => e.Sexe == sexeFilter)
                        .WhereIf(input.MinDateDeNaissanceFilter != null, e => e.DateDeNaissance >= input.MinDateDeNaissanceFilter)
                        .WhereIf(input.MaxDateDeNaissanceFilter != null, e => e.DateDeNaissance <= input.MaxDateDeNaissanceFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LieuDeNaissanceFilter), e => e.LieuDeNaissance == input.LieuDeNaissanceFilter)
                        .WhereIf(input.SituationMatrimonialeFilter.HasValue && input.SituationMatrimonialeFilter > -1, e => e.SituationMatrimoniale == situationMatrimonialeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CINFilter), e => e.CIN == input.CINFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AdresseFilter), e => e.Adresse == input.AdresseFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.VilleFilter), e => e.Ville == input.VilleFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RegionFilter), e => e.Region == input.RegionFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PaysFilter), e => e.Pays == input.PaysFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmailFilter), e => e.Email == input.EmailFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TelephoneFilter), e => e.Telephone == input.TelephoneFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ChambreReferenceFilter), e => e.ChambreFk != null && e.ChambreFk.Reference == input.ChambreReferenceFilter);

            var query = (from o in filteredEtudiants
                         join o1 in _lookup_chambreRepository.GetAll() on o.ChambreId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         select new GetEtudiantForViewDto()
                         {
                             Etudiant = new EtudiantDto
                             {
                                 INE = o.INE,
                                 CodeEtudiant = o.CodeEtudiant,
                                 Prenom = o.Prenom,
                                 Nom = o.Nom,
                                 Sexe = o.Sexe,
                                 DateDeNaissance = o.DateDeNaissance,
                                 LieuDeNaissance = o.LieuDeNaissance,
                                 SituationMatrimoniale = o.SituationMatrimoniale,
                                 CIN = o.CIN,
                                 Adresse = o.Adresse,
                                 Ville = o.Ville,
                                 Region = o.Region,
                                 Pays = o.Pays,
                                 Email = o.Email,
                                 Telephone = o.Telephone,
                                 Id = o.Id
                             },
                             ChambreReference = s1 == null || s1.Reference == null ? "" : s1.Reference.ToString()
                         });

            var etudiantListDtos = await query.ToListAsync();

            return _etudiantsExcelExporter.ExportToFile(etudiantListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Etudiants)]
        public async Task<List<EtudiantChambreLookupTableDto>> GetAllChambreForTableDropdown()
        {
            return await _lookup_chambreRepository.GetAll()
                .Select(chambre => new EtudiantChambreLookupTableDto
                {
                    Id = chambre.Id,
                    DisplayName = chambre == null || chambre.Reference == null ? "" : chambre.Reference.ToString()
                }).ToListAsync();
        }

    }
}