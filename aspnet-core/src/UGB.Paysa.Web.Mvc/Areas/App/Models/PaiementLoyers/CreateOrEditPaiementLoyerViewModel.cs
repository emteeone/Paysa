using UGB.Paysa.UGB.Paysa.Chambres.Dtos;
using System.Collections.Generic;

using Abp.Extensions;

namespace UGB.Paysa.Web.Areas.App.Models.PaiementLoyers
{
    public class CreateOrEditPaiementLoyerModalViewModel
    {
        public CreateOrEditPaiementLoyerDto PaiementLoyer { get; set; }

        public string ChambreReference { get; set; }

        public string OperationCodeOperation { get; set; }

        public List<PaiementLoyerOperationLookupTableDto> PaiementLoyerOperationList { get; set; }

        public bool IsEditMode => !PaiementLoyer.Id.IsNullOrWhiteSpace();
    }
}