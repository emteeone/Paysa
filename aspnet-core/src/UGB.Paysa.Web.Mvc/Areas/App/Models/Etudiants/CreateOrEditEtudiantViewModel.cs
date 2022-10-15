using UGB.Paysa.Wallet.Etudiants.Dtos;
using System.Collections.Generic;

using Abp.Extensions;

namespace UGB.Paysa.Web.Areas.App.Models.Etudiants
{
    public class CreateOrEditEtudiantModalViewModel
    {
        public CreateOrEditEtudiantDto Etudiant { get; set; }

        public string ChambreReference { get; set; }

        public List<EtudiantChambreLookupTableDto> EtudiantChambreList { get; set; }

        public bool IsEditMode => !Etudiant.Id.IsNullOrWhiteSpace();
    }
}