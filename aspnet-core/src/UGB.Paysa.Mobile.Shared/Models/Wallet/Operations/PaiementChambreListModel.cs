using System;
using System.ComponentModel;
using Abp.AutoMapper;
using UGB.Paysa.Authorization.Users.Dto;
using UGB.Paysa.UGB.Paysa.Chambres.Dtos;
using UGB.Paysa.Wallet.Operations.Dtos;
using Xamarin.Forms;

namespace UGB.Paysa.Models.Users
{
    [AutoMapFrom(typeof(GetPaiementLoyerForViewDto))]
    public class PaiementChambreListModel : GetPaiementLoyerForViewDto
    {
        public string Mois => PaiementLoyer.Mois;
        public string CourtMois => PaiementLoyer.Mois.Substring(0,3).ToUpper();
        public int Annee => PaiementLoyer.Annee;
        public int NombreDeMoisArriere => DateTime.Now.Month - OperationDateOperation.Month;
        public string Title => "Paiement Chambre";

    }
}