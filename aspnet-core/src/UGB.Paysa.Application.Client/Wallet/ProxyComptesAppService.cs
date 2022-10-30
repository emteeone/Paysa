using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UGB.Paysa.Dto;
using UGB.Paysa.Wallet.Comptes;
using UGB.Paysa.Wallet.Comptes.Dtos;

namespace UGB.Paysa.Wallet
{
    public class ProxyComptesAppService : ProxyAppServiceBase, IComptesAppService
    {
        public Task CreateOrEdit(CreateOrEditCompteDto input)
        {
            throw new NotImplementedException();
        }

        public Task<GetCompteForViewDto> CrediterCompte(EditSoldeCompteDto input)
        {
            throw new NotImplementedException();
        }

        public Task<GetCompteForViewDto> Debiter(EditSoldeCompteDto input)
        {
            throw new NotImplementedException();
        }

        public Task Delete(EntityDto<string> input)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResultDto<GetCompteForViewDto>> GetAll(GetAllComptesInput input)
        {
            throw new NotImplementedException();
        }

        public Task<List<CompteEtudiantLookupTableDto>> GetAllEtudiantForTableDropdown()
        {
            throw new NotImplementedException();
        }

        public Task<double> GetCompteBalanceById(EntityDto<string> input)
        {
            throw new NotImplementedException();
        }

        public async Task<GetCompteForViewDto> GetCompteBalanceByUserId(EntityDto<long> userId)
        {
            return await ApiClient.GetAsync<GetCompteForViewDto>(GetEndpoint(nameof(GetCompteBalanceByUserId)), userId);
        }

        public Task<GetCompteForEditOutput> GetCompteForEdit(EntityDto<string> input)
        {
            throw new NotImplementedException();
        }

        public Task<GetCompteForViewDto> GetCompteForView(string id)
        {
            throw new NotImplementedException();
        }

        public Task<FileDto> GetComptesToExcel(GetAllComptesForExcelInput input)
        {
            throw new NotImplementedException();
        }
    }
}
