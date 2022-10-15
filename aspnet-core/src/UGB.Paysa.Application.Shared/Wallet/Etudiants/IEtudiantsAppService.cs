using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UGB.Paysa.Wallet.Etudiants.Dtos;
using UGB.Paysa.Dto;
using System.Collections.Generic;

namespace UGB.Paysa.Wallet.Etudiants
{
    public interface IEtudiantsAppService : IApplicationService
    {
        Task<PagedResultDto<GetEtudiantForViewDto>> GetAll(GetAllEtudiantsInput input);

        Task<GetEtudiantForViewDto> GetEtudiantForView(string id);

        Task<GetEtudiantForEditOutput> GetEtudiantForEdit(EntityDto<string> input);

        Task CreateOrEdit(CreateOrEditEtudiantDto input);

        Task Delete(EntityDto<string> input);

        Task<FileDto> GetEtudiantsToExcel(GetAllEtudiantsForExcelInput input);

        Task<List<EtudiantChambreLookupTableDto>> GetAllChambreForTableDropdown();

    }
}