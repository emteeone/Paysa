using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UGB.Paysa.Web.Areas.App.Models.Operations;
using UGB.Paysa.Web.Controllers;
using UGB.Paysa.Authorization;
using UGB.Paysa.Wallet.Operations;
using UGB.Paysa.Wallet.Operations.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace UGB.Paysa.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Operations)]
    public class OperationsController : PaysaControllerBase
    {
        private readonly IOperationsAppService _operationsAppService;

        public OperationsController(IOperationsAppService operationsAppService)
        {
            _operationsAppService = operationsAppService;

        }

        public ActionResult Index()
        {
            var model = new OperationsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Operations_Create, AppPermissions.Pages_Operations_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(string id)
        {
            GetOperationForEditOutput getOperationForEditOutput;

            if (!id.IsNullOrWhiteSpace())
            {
                getOperationForEditOutput = await _operationsAppService.GetOperationForEdit(new EntityDto<string> { Id = (string)id });
            }
            else
            {
                getOperationForEditOutput = new GetOperationForEditOutput
                {
                    Operation = new CreateOrEditOperationDto()
                };
                getOperationForEditOutput.Operation.DateOperation = DateTime.Now;
                getOperationForEditOutput.Operation.CodeOperation = await _operationsAppService.GenerateCodeOperation();
;            }

            var viewModel = new CreateOrEditOperationModalViewModel()
            {
                Operation = getOperationForEditOutput.Operation,
                CompteNumeroCompte = getOperationForEditOutput.CompteNumeroCompte,
                TypeOperationNom = getOperationForEditOutput.TypeOperationNom,
                OperationTypeOperationList = await _operationsAppService.GetAllTypeOperationForTableDropdown(),

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewOperationModal(string id)
        {
            var getOperationForViewDto = await _operationsAppService.GetOperationForView(id);

            var model = new OperationViewModel()
            {
                Operation = getOperationForViewDto.Operation
                ,
                CompteNumeroCompte = getOperationForViewDto.CompteNumeroCompte

                ,
                TypeOperationNom = getOperationForViewDto.TypeOperationNom

            };

            return PartialView("_ViewOperationModal", model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Operations_Create, AppPermissions.Pages_Operations_Edit)]
        public PartialViewResult CompteLookupTableModal(string? id, string displayName)
        {
            var viewModel = new OperationCompteLookupTableViewModel()
            {
                Id = id,
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_OperationCompteLookupTableModal", viewModel);
        }

    }
}