using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UGB.Paysa.Wallet.Chambres.Dtos;
using UGB.Paysa.Dto;

namespace UGB.Paysa.Wallet.Chambres
{
    public interface IChambresAppService : IApplicationService
    {
        Task<PagedResultDto<GetChambreForViewDto>> GetAll(GetAllChambresInput input);

        Task<GetChambreForViewDto> GetChambreForView(string id);

        Task<GetChambreForEditOutput> GetChambreForEdit(EntityDto<string> input);

        Task CreateOrEdit(CreateOrEditChambreDto input);

        Task Delete(EntityDto<string> input);

        Task<FileDto> GetChambresToExcel(GetAllChambresForExcelInput input);

    }
}