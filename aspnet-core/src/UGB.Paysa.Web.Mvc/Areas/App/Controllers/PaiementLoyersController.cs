using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UGB.Paysa.Web.Areas.App.Models.PaiementLoyers;
using UGB.Paysa.Web.Controllers;
using UGB.Paysa.Authorization;
using UGB.Paysa.UGB.Paysa.Chambres;
using UGB.Paysa.UGB.Paysa.Chambres.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace UGB.Paysa.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_PaiementLoyers)]
    public class PaiementLoyersController : PaysaControllerBase
    {
        private readonly IPaiementLoyersAppService _paiementLoyersAppService;

        public PaiementLoyersController(IPaiementLoyersAppService paiementLoyersAppService)
        {
            _paiementLoyersAppService = paiementLoyersAppService;

        }

        public ActionResult Index()
        {
            var model = new PaiementLoyersViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_PaiementLoyers_Create, AppPermissions.Pages_PaiementLoyers_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(string id)
        {
            GetPaiementLoyerForEditOutput getPaiementLoyerForEditOutput;

            if (!id.IsNullOrWhiteSpace())
            {
                getPaiementLoyerForEditOutput = await _paiementLoyersAppService.GetPaiementLoyerForEdit(new EntityDto<string> { Id = (string)id });
            }
            else
            {
                getPaiementLoyerForEditOutput = new GetPaiementLoyerForEditOutput
                {
                    PaiementLoyer = new CreateOrEditPaiementLoyerDto()
                };
            }

            var viewModel = new CreateOrEditPaiementLoyerModalViewModel()
            {
                PaiementLoyer = getPaiementLoyerForEditOutput.PaiementLoyer,
                ChambreReference = getPaiementLoyerForEditOutput.ChambreReference,
                OperationCodeOperation = getPaiementLoyerForEditOutput.OperationCodeOperation,
                PaiementLoyerOperationList = await _paiementLoyersAppService.GetAllOperationForTableDropdown(),

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewPaiementLoyerModal(string id)
        {
            var getPaiementLoyerForViewDto = await _paiementLoyersAppService.GetPaiementLoyerForView(id);

            var model = new PaiementLoyerViewModel()
            {
                PaiementLoyer = getPaiementLoyerForViewDto.PaiementLoyer
                ,
                ChambreReference = getPaiementLoyerForViewDto.ChambreReference

                ,
                OperationCodeOperation = getPaiementLoyerForViewDto.OperationCodeOperation

            };

            return PartialView("_ViewPaiementLoyerModal", model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_PaiementLoyers_Create, AppPermissions.Pages_PaiementLoyers_Edit)]
        public PartialViewResult ChambreLookupTableModal(string? id, string displayName)
        {
            var viewModel = new PaiementLoyerChambreLookupTableViewModel()
            {
                Id = id,
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_PaiementLoyerChambreLookupTableModal", viewModel);
        }

    }
}