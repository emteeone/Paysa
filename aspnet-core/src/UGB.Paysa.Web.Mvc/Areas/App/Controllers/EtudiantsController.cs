using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UGB.Paysa.Web.Areas.App.Models.Etudiants;
using UGB.Paysa.Web.Controllers;
using UGB.Paysa.Authorization;
using UGB.Paysa.Wallet.Etudiants;
using UGB.Paysa.Wallet.Etudiants.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace UGB.Paysa.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Etudiants)]
    public class EtudiantsController : PaysaControllerBase
    {
        private readonly IEtudiantsAppService _etudiantsAppService;

        public EtudiantsController(IEtudiantsAppService etudiantsAppService)
        {
            _etudiantsAppService = etudiantsAppService;

        }

        public ActionResult Index()
        {
            var model = new EtudiantsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Etudiants_Create, AppPermissions.Pages_Etudiants_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(string id)
        {
            GetEtudiantForEditOutput getEtudiantForEditOutput;

            if (!id.IsNullOrWhiteSpace())
            {
                getEtudiantForEditOutput = await _etudiantsAppService.GetEtudiantForEdit(new EntityDto<string> { Id = (string)id });
            }
            else
            {
                getEtudiantForEditOutput = new GetEtudiantForEditOutput
                {
                    Etudiant = new CreateOrEditEtudiantDto()
                };
                getEtudiantForEditOutput.Etudiant.DateDeNaissance = DateTime.Now;
            }

            var viewModel = new CreateOrEditEtudiantModalViewModel()
            {
                Etudiant = getEtudiantForEditOutput.Etudiant,
                ChambreReference = getEtudiantForEditOutput.ChambreReference,
                UserName = getEtudiantForEditOutput.UserName,
                EtudiantChambreList = await _etudiantsAppService.GetAllChambreForTableDropdown(),

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewEtudiantModal(string id)
        {
            var getEtudiantForViewDto = await _etudiantsAppService.GetEtudiantForView(id);

            var model = new EtudiantViewModel()
            {
                Etudiant = getEtudiantForViewDto.Etudiant
                ,
                ChambreReference = getEtudiantForViewDto.ChambreReference

                ,
                UserName = getEtudiantForViewDto.UserName

            };

            return PartialView("_ViewEtudiantModal", model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Etudiants_Create, AppPermissions.Pages_Etudiants_Edit)]
        public PartialViewResult UserLookupTableModal(long? id, string displayName)
        {
            var viewModel = new EtudiantUserLookupTableViewModel()
            {
                Id = id,
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_EtudiantUserLookupTableModal", viewModel);
        }

    }
}