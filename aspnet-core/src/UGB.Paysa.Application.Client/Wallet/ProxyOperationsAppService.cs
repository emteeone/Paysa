using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UGB.Paysa.Authorization.Users.Dto;
using UGB.Paysa.Dto;
using UGB.Paysa.Wallet.Operations;
using UGB.Paysa.Wallet.Operations.Dtos;

namespace UGB.Paysa.Wallet
{
    public class ProxyOperationsAppService : ProxyAppServiceBase, IOperationsAppService
    {
        public Task CreateOrEdit(CreateOrEditOperationDto input)
        {
            throw new NotImplementedException();
        }

        public Task Delete(EntityDto<string> input)
        {
            throw new NotImplementedException();
        }

        public async  Task<PagedResultDto<GetOperationForViewDto>> GetAll(GetAllOperationsInput input)
        {
            return await ApiClient.GetAsync<PagedResultDto<GetOperationForViewDto>>(GetEndpoint(nameof(GetAll)), input);
        }

        public Task<List<OperationCompteLookupTableDto>> GetAllCompteForTableDropdown()
        {
            throw new NotImplementedException();
        }

        public Task<GetOperationForEditOutput> GetOperationForEdit(EntityDto<string> input)
        {
            throw new NotImplementedException();
        }

        public Task<GetOperationForViewDto> GetOperationForView(string id)
        {
            throw new NotImplementedException();
        }

        public Task<FileDto> GetOperationsToExcel(GetAllOperationsForExcelInput input)
        {
            throw new NotImplementedException();
        }
    }
}
