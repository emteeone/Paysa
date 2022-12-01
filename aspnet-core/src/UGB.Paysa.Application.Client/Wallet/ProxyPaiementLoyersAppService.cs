using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UGB.Paysa.Authorization.Users.Dto;
using UGB.Paysa.Dto;
using UGB.Paysa.UGB.Paysa.Chambres;
using UGB.Paysa.UGB.Paysa.Chambres.Dtos;
using UGB.Paysa.Wallet.Operations;
using UGB.Paysa.Wallet.Operations.Dtos;

namespace UGB.Paysa.Wallet
{
    public class ProxyAPiementLoyersAppService : ProxyAppServiceBase, IPaiementLoyersAppService
    {
        public Task CreateOrEdit(CreateOrEditPaiementLoyerDto input)
        {
            throw new NotImplementedException();
        }

        public Task Delete(EntityDto<string> input)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResultDto<GetPaiementLoyerForViewDto>> GetAll(GetAllPaiementLoyersInput input)
        {
            return await ApiClient.GetAsync<PagedResultDto<GetPaiementLoyerForViewDto>>(GetEndpoint(nameof(GetAll)), input);
        }

        public Task<PagedResultDto<PaiementLoyerChambreLookupTableDto>> GetAllChambreForLookupTable(UGB.Paysa.Chambres.Dtos.GetAllForLookupTableInput input)
        {
            throw new NotImplementedException();
        }

        public Task<List<PaiementLoyerOperationLookupTableDto>> GetAllOperationForTableDropdown()
        {
            throw new NotImplementedException();
        }

        public Task<GetPaiementLoyerForEditOutput> GetPaiementLoyerForEdit(EntityDto<string> input)
        {
            throw new NotImplementedException();
        }

        public Task<GetPaiementLoyerForViewDto> GetPaiementLoyerForView(string id)
        {
            throw new NotImplementedException();
        }

        public Task<FileDto> GetPaiementLoyersToExcel(GetAllPaiementLoyersForExcelInput input)
        {
            throw new NotImplementedException();
        }
    }
}
