using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UGB.Paysa.Web.Areas.App.Models.TypeOperations;
using UGB.Paysa.Web.Controllers;
using UGB.Paysa.Authorization;
using UGB.Paysa.Wallet.Operations;
using UGB.Paysa.Wallet.Operations.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace UGB.Paysa.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_TypeOperations)]
    public class TypeOperationsController : PaysaControllerBase
    {
        private readonly ITypeOperationsAppService _typeOperationsAppService;

        public TypeOperationsController(ITypeOperationsAppService typeOperationsAppService)
        {
            _typeOperationsAppService = typeOperationsAppService;

        }

        public ActionResult Index()
        {
            var model = new TypeOperationsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_TypeOperations_Create, AppPermissions.Pages_TypeOperations_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(string id)
        {
            GetTypeOperationForEditOutput getTypeOperationForEditOutput;

            if (!id.IsNullOrWhiteSpace())
            {
                getTypeOperationForEditOutput = await _typeOperationsAppService.GetTypeOperationForEdit(new EntityDto<string> { Id = (string)id });
            }
            else
            {
                getTypeOperationForEditOutput = new GetTypeOperationForEditOutput
                {
                    TypeOperation = new CreateOrEditTypeOperationDto()
                };
            }

            var viewModel = new CreateOrEditTypeOperationModalViewModel()
            {
                TypeOperation = getTypeOperationForEditOutput.TypeOperation,

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewTypeOperationModal(string id)
        {
            var getTypeOperationForViewDto = await _typeOperationsAppService.GetTypeOperationForView(id);

            var model = new TypeOperationViewModel()
            {
                TypeOperation = getTypeOperationForViewDto.TypeOperation
            };

            return PartialView("_ViewTypeOperationModal", model);
        }

    }
}