using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UGB.Paysa.Web.Areas.App.Models.Chambres;
using UGB.Paysa.Web.Controllers;
using UGB.Paysa.Authorization;
using UGB.Paysa.Wallet.Chambres;
using UGB.Paysa.Wallet.Chambres.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace UGB.Paysa.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Chambres)]
    public class ChambresController : PaysaControllerBase
    {
        private readonly IChambresAppService _chambresAppService;

        public ChambresController(IChambresAppService chambresAppService)
        {
            _chambresAppService = chambresAppService;

        }

        public ActionResult Index()
        {
            var model = new ChambresViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Chambres_Create, AppPermissions.Pages_Chambres_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(string id)
        {
            GetChambreForEditOutput getChambreForEditOutput;

            if (!id.IsNullOrWhiteSpace())
            {
                getChambreForEditOutput = await _chambresAppService.GetChambreForEdit(new EntityDto<string> { Id = (string)id });
            }
            else
            {
                getChambreForEditOutput = new GetChambreForEditOutput
                {
                    Chambre = new CreateOrEditChambreDto()
                };
            }

            var viewModel = new CreateOrEditChambreModalViewModel()
            {
                Chambre = getChambreForEditOutput.Chambre,

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewChambreModal(string id)
        {
            var getChambreForViewDto = await _chambresAppService.GetChambreForView(id);

            var model = new ChambreViewModel()
            {
                Chambre = getChambreForViewDto.Chambre
            };

            return PartialView("_ViewChambreModal", model);
        }

    }
}