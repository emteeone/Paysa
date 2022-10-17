using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UGB.Paysa.Web.Areas.App.Models.Terminaux;
using UGB.Paysa.Web.Controllers;
using UGB.Paysa.Authorization;
using UGB.Paysa.Wallet.Tools;
using UGB.Paysa.Wallet.Tools.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace UGB.Paysa.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Terminaux)]
    public class TerminauxController : PaysaControllerBase
    {
        private readonly ITerminauxAppService _terminauxAppService;

        public TerminauxController(ITerminauxAppService terminauxAppService)
        {
            _terminauxAppService = terminauxAppService;

        }

        public ActionResult Index()
        {
            var model = new TerminauxViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Terminaux_Create, AppPermissions.Pages_Terminaux_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(string id)
        {
            GetTerminalForEditOutput getTerminalForEditOutput;

            if (!id.IsNullOrWhiteSpace())
            {
                getTerminalForEditOutput = await _terminauxAppService.GetTerminalForEdit(new EntityDto<string> { Id = (string)id });
            }
            else
            {
                getTerminalForEditOutput = new GetTerminalForEditOutput
                {
                    Terminal = new CreateOrEditTerminalDto()
                };
            }

            var viewModel = new CreateOrEditTerminalModalViewModel()
            {
                Terminal = getTerminalForEditOutput.Terminal,

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewTerminalModal(string id)
        {
            var getTerminalForViewDto = await _terminauxAppService.GetTerminalForView(id);

            var model = new TerminalViewModel()
            {
                Terminal = getTerminalForViewDto.Terminal
            };

            return PartialView("_ViewTerminalModal", model);
        }

    }
}