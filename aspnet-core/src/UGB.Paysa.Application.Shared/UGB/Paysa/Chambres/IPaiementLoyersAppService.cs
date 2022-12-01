using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UGB.Paysa.UGB.Paysa.Chambres.Dtos;
using UGB.Paysa.Dto;
using System.Collections.Generic;

namespace UGB.Paysa.UGB.Paysa.Chambres
{
    public interface IPaiementLoyersAppService : IApplicationService
    {
        Task<PagedResultDto<GetPaiementLoyerForViewDto>> GetAll(GetAllPaiementLoyersInput input);

        Task<GetPaiementLoyerForViewDto> GetPaiementLoyerForView(string id);

        Task<GetPaiementLoyerForEditOutput> GetPaiementLoyerForEdit(EntityDto<string> input);

        Task CreateOrEdit(CreateOrEditPaiementLoyerDto input);

        Task Delete(EntityDto<string> input);

        Task<FileDto> GetPaiementLoyersToExcel(GetAllPaiementLoyersForExcelInput input);

        Task<PagedResultDto<PaiementLoyerChambreLookupTableDto>> GetAllChambreForLookupTable(GetAllForLookupTableInput input);

        Task<List<PaiementLoyerOperationLookupTableDto>> GetAllOperationForTableDropdown();

    }
}