using UGB.Paysa.Wallet.Operations.Dtos;
using System.Collections.Generic;

using Abp.Extensions;

namespace UGB.Paysa.Web.Areas.App.Models.Operations
{
    public class CreateOrEditOperationModalViewModel
    {
        public CreateOrEditOperationDto Operation { get; set; }

        public string CompteNumeroCompte { get; set; }

        public List<OperationCompteLookupTableDto> OperationCompteList { get; set; }

        public bool IsEditMode => !Operation.Id.IsNullOrWhiteSpace();
    }
}