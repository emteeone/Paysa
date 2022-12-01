using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UGB.Paysa.Authorization.Users.Dto;
using UGB.Paysa.Dto;
using UGB.Paysa.Wallet.Chambres;
using UGB.Paysa.Wallet.Chambres.Dtos;
using UGB.Paysa.Wallet.Comptes.Dtos;
using UGB.Paysa.Wallet.Operations;
using UGB.Paysa.Wallet.Operations.Dtos;

namespace UGB.Paysa.Wallet
{
    public class ProxyChambresAppService : ProxyAppServiceBase, IChambresAppService
    {
        public Task CreateOrEdit(CreateOrEditChambreDto input)
        {
            throw new NotImplementedException();
        }

        public Task Delete(EntityDto<string> input)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResultDto<GetChambreForViewDto>> GetAll(GetAllChambresInput input)
        {
            throw new NotImplementedException();
        }

        public async Task<GetChambreForViewDto> GetChambreByUserId(EntityDto<long> input)
        {
            return await ApiClient.GetAsync<GetChambreForViewDto>(GetEndpoint(nameof(GetChambreByUserId)), input);
        }

        public Task<GetChambreForEditOutput> GetChambreForEdit(EntityDto<string> input)
        {
            throw new NotImplementedException();
        }

        public Task<GetChambreForViewDto> GetChambreForView(string id)
        {
            throw new NotImplementedException();
        }

        public Task<FileDto> GetChambresToExcel(GetAllChambresForExcelInput input)
        {
            throw new NotImplementedException();
        }
    }
}
