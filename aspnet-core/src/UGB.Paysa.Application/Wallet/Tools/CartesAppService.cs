using UGB.Paysa.Wallet.Comptes;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using UGB.Paysa.Wallet.Tools.Exporting;
using UGB.Paysa.Wallet.Tools.Dtos;
using UGB.Paysa.Dto;
using Abp.Application.Services.Dto;
using UGB.Paysa.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using UGB.Paysa.Storage;

namespace UGB.Paysa.Wallet.Tools
{
    [AbpAuthorize(AppPermissions.Pages_Cartes)]
    public class CartesAppService : PaysaAppServiceBase, ICartesAppService
    {
        private readonly IRepository<Carte, string> _carteRepository;
        private readonly ICartesExcelExporter _cartesExcelExporter;
        private readonly IRepository<Compte, string> _lookup_compteRepository;

        public CartesAppService(IRepository<Carte, string> carteRepository, ICartesExcelExporter cartesExcelExporter, IRepository<Compte, string> lookup_compteRepository)
        {
            _carteRepository = carteRepository;
            _cartesExcelExporter = cartesExcelExporter;
            _lookup_compteRepository = lookup_compteRepository;

        }

        public async Task<PagedResultDto<GetCarteForViewDto>> GetAll(GetAllCartesInput input)
        {

            var filteredCartes = _carteRepository.GetAll()
                        .Include(e => e.CompteFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.UID.Contains(input.Filter) || e.NumeroCarte.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UIDFilter), e => e.UID == input.UIDFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NumeroCarteFilter), e => e.NumeroCarte == input.NumeroCarteFilter)
                        .WhereIf(input.IsActiveFilter.HasValue && input.IsActiveFilter > -1, e => (input.IsActiveFilter == 1 && e.IsActive) || (input.IsActiveFilter == 0 && !e.IsActive))
                        .WhereIf(input.MinDateDelivranceFilter != null, e => e.DateDelivrance >= input.MinDateDelivranceFilter)
                        .WhereIf(input.MaxDateDelivranceFilter != null, e => e.DateDelivrance <= input.MaxDateDelivranceFilter)
                        .WhereIf(input.MinDateExpirationFilter != null, e => e.DateExpiration >= input.MinDateExpirationFilter)
                        .WhereIf(input.MaxDateExpirationFilter != null, e => e.DateExpiration <= input.MaxDateExpirationFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CompteNumeroCompteFilter), e => e.CompteFk != null && e.CompteFk.NumeroCompte == input.CompteNumeroCompteFilter);

            var pagedAndFilteredCartes = filteredCartes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var cartes = from o in pagedAndFilteredCartes
                         join o1 in _lookup_compteRepository.GetAll() on o.CompteId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         select new
                         {

                             o.UID,
                             o.NumeroCarte,
                             o.IsActive,
                             o.DateDelivrance,
                             o.DateExpiration,
                             Id = o.Id,
                             CompteNumeroCompte = s1 == null || s1.NumeroCompte == null ? "" : s1.NumeroCompte.ToString()
                         };

            var totalCount = await filteredCartes.CountAsync();

            var dbList = await cartes.ToListAsync();
            var results = new List<GetCarteForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetCarteForViewDto()
                {
                    Carte = new CarteDto
                    {

                        UID = o.UID,
                        NumeroCarte = o.NumeroCarte,
                        IsActive = o.IsActive,
                        DateDelivrance = o.DateDelivrance,
                        DateExpiration = o.DateExpiration,
                        Id = o.Id,
                    },
                    CompteNumeroCompte = o.CompteNumeroCompte
                };

                results.Add(res);
            }

            return new PagedResultDto<GetCarteForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetCarteForViewDto> GetCarteForView(string id)
        {
            var carte = await _carteRepository.GetAsync(id);

            var output = new GetCarteForViewDto { Carte = ObjectMapper.Map<CarteDto>(carte) };

            if (output.Carte.CompteId != null)
            {
                var _lookupCompte = await _lookup_compteRepository.FirstOrDefaultAsync((string)output.Carte.CompteId);
                output.CompteNumeroCompte = _lookupCompte?.NumeroCompte?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Cartes_Edit)]
        public async Task<GetCarteForEditOutput> GetCarteForEdit(EntityDto<string> input)
        {
            var carte = await _carteRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetCarteForEditOutput { Carte = ObjectMapper.Map<CreateOrEditCarteDto>(carte) };

            if (output.Carte.CompteId != null)
            {
                var _lookupCompte = await _lookup_compteRepository.FirstOrDefaultAsync((string)output.Carte.CompteId);
                output.CompteNumeroCompte = _lookupCompte?.NumeroCompte?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditCarteDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Cartes_Create)]
        protected virtual async Task Create(CreateOrEditCarteDto input)
        {
            var carte = ObjectMapper.Map<Carte>(input);

            if (AbpSession.TenantId != null)
            {
                carte.TenantId = (int?)AbpSession.TenantId;
            }

            if (carte.Id.IsNullOrWhiteSpace())
            {
                carte.Id = Guid.NewGuid().ToString();
            }

            await _carteRepository.InsertAsync(carte);

        }

        [AbpAuthorize(AppPermissions.Pages_Cartes_Edit)]
        protected virtual async Task Update(CreateOrEditCarteDto input)
        {
            var carte = await _carteRepository.FirstOrDefaultAsync((string)input.Id);
            ObjectMapper.Map(input, carte);

        }

        [AbpAuthorize(AppPermissions.Pages_Cartes_Delete)]
        public async Task Delete(EntityDto<string> input)
        {
            await _carteRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetCartesToExcel(GetAllCartesForExcelInput input)
        {

            var filteredCartes = _carteRepository.GetAll()
                        .Include(e => e.CompteFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.UID.Contains(input.Filter) || e.NumeroCarte.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UIDFilter), e => e.UID == input.UIDFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NumeroCarteFilter), e => e.NumeroCarte == input.NumeroCarteFilter)
                        .WhereIf(input.IsActiveFilter.HasValue && input.IsActiveFilter > -1, e => (input.IsActiveFilter == 1 && e.IsActive) || (input.IsActiveFilter == 0 && !e.IsActive))
                        .WhereIf(input.MinDateDelivranceFilter != null, e => e.DateDelivrance >= input.MinDateDelivranceFilter)
                        .WhereIf(input.MaxDateDelivranceFilter != null, e => e.DateDelivrance <= input.MaxDateDelivranceFilter)
                        .WhereIf(input.MinDateExpirationFilter != null, e => e.DateExpiration >= input.MinDateExpirationFilter)
                        .WhereIf(input.MaxDateExpirationFilter != null, e => e.DateExpiration <= input.MaxDateExpirationFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CompteNumeroCompteFilter), e => e.CompteFk != null && e.CompteFk.NumeroCompte == input.CompteNumeroCompteFilter);

            var query = (from o in filteredCartes
                         join o1 in _lookup_compteRepository.GetAll() on o.CompteId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         select new GetCarteForViewDto()
                         {
                             Carte = new CarteDto
                             {
                                 UID = o.UID,
                                 NumeroCarte = o.NumeroCarte,
                                 IsActive = o.IsActive,
                                 DateDelivrance = o.DateDelivrance,
                                 DateExpiration = o.DateExpiration,
                                 Id = o.Id
                             },
                             CompteNumeroCompte = s1 == null || s1.NumeroCompte == null ? "" : s1.NumeroCompte.ToString()
                         });

            var carteListDtos = await query.ToListAsync();

            return _cartesExcelExporter.ExportToFile(carteListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Cartes)]
        public async Task<List<CarteCompteLookupTableDto>> GetAllCompteForTableDropdown()
        {
            return await _lookup_compteRepository.GetAll()
                .Select(compte => new CarteCompteLookupTableDto
                {
                    Id = compte.Id,
                    DisplayName = compte == null || compte.NumeroCompte == null ? "" : compte.NumeroCompte.ToString()
                }).ToListAsync();
        }

    }
}