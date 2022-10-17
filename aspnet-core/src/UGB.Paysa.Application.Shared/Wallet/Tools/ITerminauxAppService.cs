using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UGB.Paysa.Wallet.Tools.Dtos;
using UGB.Paysa.Dto;

namespace UGB.Paysa.Wallet.Tools
{
    public interface ITerminauxAppService : IApplicationService
    {
        Task<PagedResultDto<GetTerminalForViewDto>> GetAll(GetAllTerminauxInput input);

        Task<GetTerminalForViewDto> GetTerminalForView(string id);

        Task<GetTerminalForEditOutput> GetTerminalForEdit(EntityDto<string> input);

        Task CreateOrEdit(CreateOrEditTerminalDto input);

        Task Delete(EntityDto<string> input);

        Task<FileDto> GetTerminauxToExcel(GetAllTerminauxForExcelInput input);

    }
}