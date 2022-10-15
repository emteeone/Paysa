using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UGB.Paysa.Web.Areas.App.Models.Comptes;
using UGB.Paysa.Web.Controllers;
using UGB.Paysa.Authorization;
using UGB.Paysa.Wallet.Comptes;
using UGB.Paysa.Wallet.Comptes.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace UGB.Paysa.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Comptes)]
    public class ComptesController : PaysaControllerBase
    {
        private readonly IComptesAppService _comptesAppService;

        public ComptesController(IComptesAppService comptesAppService)
        {
            _comptesAppService = comptesAppService;

        }

        public ActionResult Index()
        {
            var model = new ComptesViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Comptes_Create, AppPermissions.Pages_Comptes_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(string id)
        {
            GetCompteForEditOutput getCompteForEditOutput;

            if (!id.IsNullOrWhiteSpace())
            {
                getCompteForEditOutput = await _comptesAppService.GetCompteForEdit(new EntityDto<string> { Id = (string)id });
            }
            else
            {
                getCompteForEditOutput = new GetCompteForEditOutput
                {
                    Compte = new CreateOrEditCompteDto()
                };
                getCompteForEditOutput.Compte.DateCreation = DateTime.Now;
            }

            var viewModel = new CreateOrEditCompteModalViewModel()
            {
                Compte = getCompteForEditOutput.Compte,
                EtudiantCodeEtudiant = getCompteForEditOutput.EtudiantCodeEtudiant,
                CompteEtudiantList = await _comptesAppService.GetAllEtudiantForTableDropdown(),

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewCompteModal(string id)
        {
            var getCompteForViewDto = await _comptesAppService.GetCompteForView(id);

            var model = new CompteViewModel()
            {
                Compte = getCompteForViewDto.Compte
                ,
                EtudiantCodeEtudiant = getCompteForViewDto.EtudiantCodeEtudiant

            };

            return PartialView("_ViewCompteModal", model);
        }

    }
}