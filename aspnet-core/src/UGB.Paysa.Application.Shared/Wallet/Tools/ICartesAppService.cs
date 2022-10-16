using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UGB.Paysa.Wallet.Tools.Dtos;
using UGB.Paysa.Dto;
using System.Collections.Generic;

namespace UGB.Paysa.Wallet.Tools
{
    public interface ICartesAppService : IApplicationService
    {
        Task<PagedResultDto<GetCarteForViewDto>> GetAll(GetAllCartesInput input);

        Task<GetCarteForViewDto> GetCarteForView(string id);

        Task<GetCarteForEditOutput> GetCarteForEdit(EntityDto<string> input);

        Task CreateOrEdit(CreateOrEditCarteDto input);

        Task Delete(EntityDto<string> input);

        Task<FileDto> GetCartesToExcel(GetAllCartesForExcelInput input);

        Task<List<CarteCompteLookupTableDto>> GetAllCompteForTableDropdown();

    }
}