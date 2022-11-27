using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UGB.Paysa.Wallet.Operations.Dtos;
using UGB.Paysa.Dto;

namespace UGB.Paysa.Wallet.Operations
{
    public interface ITypeOperationsAppService : IApplicationService
    {
        Task<PagedResultDto<GetTypeOperationForViewDto>> GetAll(GetAllTypeOperationsInput input);

        Task<GetTypeOperationForViewDto> GetTypeOperationForView(string id);

        Task<GetTypeOperationForEditOutput> GetTypeOperationForEdit(EntityDto<string> input);

        Task CreateOrEdit(CreateOrEditTypeOperationDto input);

        Task Delete(EntityDto<string> input);

        Task<FileDto> GetTypeOperationsToExcel(GetAllTypeOperationsForExcelInput input);

    }
}