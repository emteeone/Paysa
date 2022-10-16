using UGB.Paysa.Wallet.Tools.Dtos;
using System.Collections.Generic;

using Abp.Extensions;

namespace UGB.Paysa.Web.Areas.App.Models.Cartes
{
    public class CreateOrEditCarteModalViewModel
    {
        public CreateOrEditCarteDto Carte { get; set; }

        public string CompteNumeroCompte { get; set; }

        public List<CarteCompteLookupTableDto> CarteCompteList { get; set; }

        public bool IsEditMode => !Carte.Id.IsNullOrWhiteSpace();
    }
}