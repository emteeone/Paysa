using UGB.Paysa.Wallet.Operations.Dtos;
using System.Collections.Generic;

using Abp.Extensions;

namespace UGB.Paysa.Web.Areas.App.Models.Operations
{
    public class CreateOrEditOperationModalViewModel
    {
        public CreateOrEditOperationDto Operation { get; set; }

        public string CompteNumeroCompte { get; set; }

        public string TypeOperationNom { get; set; }

        public List<OperationTypeOperationLookupTableDto> OperationTypeOperationList { get; set; }

        public bool IsEditMode => !Operation.Id.IsNullOrWhiteSpace();
    }
}