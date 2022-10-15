using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UGB.Paysa.Wallet.Comptes.Dtos;
using UGB.Paysa.Dto;
using System.Collections.Generic;

namespace UGB.Paysa.Wallet.Comptes
{
    public interface IComptesAppService : IApplicationService
    {
        Task<PagedResultDto<GetCompteForViewDto>> GetAll(GetAllComptesInput input);

        Task<GetCompteForViewDto> GetCompteForView(string id);

        Task<GetCompteForEditOutput> GetCompteForEdit(EntityDto<string> input);

        Task CreateOrEdit(CreateOrEditCompteDto input);

        Task Delete(EntityDto<string> input);

        Task<FileDto> GetComptesToExcel(GetAllComptesForExcelInput input);

        Task<List<CompteEtudiantLookupTableDto>> GetAllEtudiantForTableDropdown();

    }
}