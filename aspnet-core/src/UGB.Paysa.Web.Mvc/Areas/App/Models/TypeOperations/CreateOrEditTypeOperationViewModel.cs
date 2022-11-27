using UGB.Paysa.Wallet.Operations.Dtos;

using Abp.Extensions;

namespace UGB.Paysa.Web.Areas.App.Models.TypeOperations
{
    public class CreateOrEditTypeOperationModalViewModel
    {
        public CreateOrEditTypeOperationDto TypeOperation { get; set; }

        public bool IsEditMode => !TypeOperation.Id.IsNullOrWhiteSpace();
    }
}