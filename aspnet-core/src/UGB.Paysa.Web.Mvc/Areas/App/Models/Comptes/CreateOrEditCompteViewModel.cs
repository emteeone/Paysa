using UGB.Paysa.Wallet.Comptes.Dtos;
using System.Collections.Generic;

using Abp.Extensions;

namespace UGB.Paysa.Web.Areas.App.Models.Comptes
{
    public class CreateOrEditCompteModalViewModel
    {
        public CreateOrEditCompteDto Compte { get; set; }

        public string EtudiantCodeEtudiant { get; set; }

        public List<CompteEtudiantLookupTableDto> CompteEtudiantList { get; set; }

        public bool IsEditMode => !Compte.Id.IsNullOrWhiteSpace();
    }
}