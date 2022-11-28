using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UGB.Paysa.Wallet.Operations.Dtos;
using UGB.Paysa.Dto;
using System.Collections.Generic;

namespace UGB.Paysa.Wallet.Operations
{
    public interface IOperationsAppService : IApplicationService
    {
        Task<PagedResultDto<GetOperationForViewDto>> GetAll(GetAllOperationsInput input);

        Task<GetOperationForViewDto> GetOperationForView(string id);

        Task<GetOperationForEditOutput> GetOperationForEdit(EntityDto<string> input);

        Task CreateOrEdit(CreateOrEditOperationDto input);

        Task Delete(EntityDto<string> input);

        Task<FileDto> GetOperationsToExcel(GetAllOperationsForExcelInput input);

        Task<PagedResultDto<OperationCompteLookupTableDto>> GetAllCompteForLookupTable(GetAllForLookupTableInput input);

        Task<List<OperationTypeOperationLookupTableDto>> GetAllTypeOperationForTableDropdown();
        Task<string> GenerateCodeOperation();

    }
}