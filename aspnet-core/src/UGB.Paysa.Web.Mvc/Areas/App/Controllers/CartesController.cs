using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UGB.Paysa.Web.Areas.App.Models.Cartes;
using UGB.Paysa.Web.Controllers;
using UGB.Paysa.Authorization;
using UGB.Paysa.Wallet.Tools;
using UGB.Paysa.Wallet.Tools.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace UGB.Paysa.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Cartes)]
    public class CartesController : PaysaControllerBase
    {
        private readonly ICartesAppService _cartesAppService;

        public CartesController(ICartesAppService cartesAppService)
        {
            _cartesAppService = cartesAppService;

        }

        public ActionResult Index()
        {
            var model = new CartesViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Cartes_Create, AppPermissions.Pages_Cartes_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(string id)
        {
            GetCarteForEditOutput getCarteForEditOutput;

            if (!id.IsNullOrWhiteSpace())
            {
                getCarteForEditOutput = await _cartesAppService.GetCarteForEdit(new EntityDto<string> { Id = (string)id });
            }
            else
            {
                getCarteForEditOutput = new GetCarteForEditOutput
                {
                    Carte = new CreateOrEditCarteDto()
                };
                getCarteForEditOutput.Carte.DateDelivrance = DateTime.Now;
                getCarteForEditOutput.Carte.DateExpiration = DateTime.Now;
            }

            var viewModel = new CreateOrEditCarteModalViewModel()
            {
                Carte = getCarteForEditOutput.Carte,
                CompteNumeroCompte = getCarteForEditOutput.CompteNumeroCompte,
                CarteCompteList = await _cartesAppService.GetAllCompteForTableDropdown(),

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewCarteModal(string id)
        {
            var getCarteForViewDto = await _cartesAppService.GetCarteForView(id);

            var model = new CarteViewModel()
            {
                Carte = getCarteForViewDto.Carte
                ,
                CompteNumeroCompte = getCarteForViewDto.CompteNumeroCompte

            };

            return PartialView("_ViewCarteModal", model);
        }

    }
}