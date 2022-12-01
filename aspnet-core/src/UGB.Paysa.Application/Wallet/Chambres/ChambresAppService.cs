using UGB.Paysa.Wallet.Enum;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using UGB.Paysa.Wallet.Chambres.Exporting;
using UGB.Paysa.Wallet.Chambres.Dtos;
using UGB.Paysa.Dto;
using Abp.Application.Services.Dto;
using UGB.Paysa.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using UGB.Paysa.Storage;
using UGB.Paysa.Wallet.Comptes.Dtos;
using UGB.Paysa.Wallet.Etudiants;

namespace UGB.Paysa.Wallet.Chambres
{
    [AbpAuthorize(AppPermissions.Pages_Chambres)]
    public class ChambresAppService : PaysaAppServiceBase, IChambresAppService
    {
        private readonly IRepository<Chambre, string> _chambreRepository;
        private readonly IRepository<Etudiant, string> _lookup_etudiantRepository;
        private readonly IChambresExcelExporter _chambresExcelExporter;

        public ChambresAppService(IRepository<Chambre, string> chambreRepository, 
            IChambresExcelExporter chambresExcelExporter,
             IRepository<Etudiant, string> lookup_etudiantRepository)
        {
            _chambreRepository = chambreRepository;
            _lookup_etudiantRepository = lookup_etudiantRepository;
            _chambresExcelExporter = chambresExcelExporter;

        }

        public async Task<PagedResultDto<GetChambreForViewDto>> GetAll(GetAllChambresInput input)
        {
            var blocFilter = input.BlocFilter.HasValue
                        ? (Bloc)input.BlocFilter
                        : default;
            var villageFilter = input.VillageFilter.HasValue
                ? (Village)input.VillageFilter
                : default;
            var campusFilter = input.CampusFilter.HasValue
                ? (Campus)input.CampusFilter
                : default;

            var filteredChambres = _chambreRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Reference.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ReferenceFilter), e => e.Reference == input.ReferenceFilter)
                        .WhereIf(input.MinNumeroChambreFilter != null, e => e.NumeroChambre >= input.MinNumeroChambreFilter)
                        .WhereIf(input.MaxNumeroChambreFilter != null, e => e.NumeroChambre <= input.MaxNumeroChambreFilter)
                        .WhereIf(input.BlocFilter.HasValue && input.BlocFilter > -1, e => e.Bloc == blocFilter)
                        .WhereIf(input.VillageFilter.HasValue && input.VillageFilter > -1, e => e.Village == villageFilter)
                        .WhereIf(input.CampusFilter.HasValue && input.CampusFilter > -1, e => e.Campus == campusFilter)
                        .WhereIf(input.MinMontantLocationFilter != null, e => e.MontantLocation >= input.MinMontantLocationFilter)
                        .WhereIf(input.MaxMontantLocationFilter != null, e => e.MontantLocation <= input.MaxMontantLocationFilter);

            var pagedAndFilteredChambres = filteredChambres
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var chambres = from o in pagedAndFilteredChambres
                           select new
                           {

                               o.Reference,
                               o.NumeroChambre,
                               o.Bloc,
                               o.Village,
                               o.Campus,
                               o.MontantLocation,
                               Id = o.Id
                           };

            var totalCount = await filteredChambres.CountAsync();

            var dbList = await chambres.ToListAsync();
            var results = new List<GetChambreForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetChambreForViewDto()
                {
                    Chambre = new ChambreDto
                    {

                        Reference = o.Reference,
                        NumeroChambre = o.NumeroChambre,
                        Bloc = o.Bloc,
                        Village = o.Village,
                        Campus = o.Campus,
                        MontantLocation = o.MontantLocation,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetChambreForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetChambreForViewDto> GetChambreForView(string id)
        {
            var chambre = await _chambreRepository.GetAsync(id);

            var output = new GetChambreForViewDto { Chambre = ObjectMapper.Map<ChambreDto>(chambre) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Chambres_Edit)]
        public async Task<GetChambreForEditOutput> GetChambreForEdit(EntityDto<string> input)
        {
            var chambre = await _chambreRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetChambreForEditOutput { Chambre = ObjectMapper.Map<CreateOrEditChambreDto>(chambre) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditChambreDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Chambres_Create)]
        protected virtual async Task Create(CreateOrEditChambreDto input)
        {
            var chambre = ObjectMapper.Map<Chambre>(input);

            if (AbpSession.TenantId != null)
            {
                chambre.TenantId = (int?)AbpSession.TenantId;
            }

            if (chambre.Id.IsNullOrWhiteSpace())
            {
                chambre.Id = Guid.NewGuid().ToString();
            }

            await _chambreRepository.InsertAsync(chambre);

        }

        [AbpAuthorize(AppPermissions.Pages_Chambres_Edit)]
        protected virtual async Task Update(CreateOrEditChambreDto input)
        {
            var chambre = await _chambreRepository.FirstOrDefaultAsync((string)input.Id);
            ObjectMapper.Map(input, chambre);

        }

        [AbpAuthorize(AppPermissions.Pages_Chambres_Delete)]
        public async Task Delete(EntityDto<string> input)
        {
            await _chambreRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetChambresToExcel(GetAllChambresForExcelInput input)
        {
            var blocFilter = input.BlocFilter.HasValue
                        ? (Bloc)input.BlocFilter
                        : default;
            var villageFilter = input.VillageFilter.HasValue
                ? (Village)input.VillageFilter
                : default;
            var campusFilter = input.CampusFilter.HasValue
                ? (Campus)input.CampusFilter
                : default;

            var filteredChambres = _chambreRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Reference.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ReferenceFilter), e => e.Reference == input.ReferenceFilter)
                        .WhereIf(input.MinNumeroChambreFilter != null, e => e.NumeroChambre >= input.MinNumeroChambreFilter)
                        .WhereIf(input.MaxNumeroChambreFilter != null, e => e.NumeroChambre <= input.MaxNumeroChambreFilter)
                        .WhereIf(input.BlocFilter.HasValue && input.BlocFilter > -1, e => e.Bloc == blocFilter)
                        .WhereIf(input.VillageFilter.HasValue && input.VillageFilter > -1, e => e.Village == villageFilter)
                        .WhereIf(input.CampusFilter.HasValue && input.CampusFilter > -1, e => e.Campus == campusFilter)
                        .WhereIf(input.MinMontantLocationFilter != null, e => e.MontantLocation >= input.MinMontantLocationFilter)
                        .WhereIf(input.MaxMontantLocationFilter != null, e => e.MontantLocation <= input.MaxMontantLocationFilter);

            var query = (from o in filteredChambres
                         select new GetChambreForViewDto()
                         {
                             Chambre = new ChambreDto
                             {
                                 Reference = o.Reference,
                                 NumeroChambre = o.NumeroChambre,
                                 Bloc = o.Bloc,
                                 Village = o.Village,
                                 Campus = o.Campus,
                                 MontantLocation = o.MontantLocation,
                                 Id = o.Id
                             }
                         });

            var chambreListDtos = await query.ToListAsync();

            return _chambresExcelExporter.ExportToFile(chambreListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Chambres_Edit)]
        public async Task<GetChambreForViewDto> GetChambreByUserId(EntityDto<long> input)
        {
            var etudiant = await _lookup_etudiantRepository.GetAll().FirstOrDefaultAsync(e => e.UserId == input.Id);
            if (etudiant == null)
            {
                throw new UserFriendlyException("Vous n'etes pas un etudiant de l'université");
            }
            var chambre = await _chambreRepository.GetAsync(etudiant.ChambreId);

            if (chambre == null)
            {
                throw new UserFriendlyException("Vous n'avez pas encore de compte");
            }
            var output = new GetChambreForViewDto { Chambre = ObjectMapper.Map<ChambreDto>(chambre) };

            return output;
        }
    }
}