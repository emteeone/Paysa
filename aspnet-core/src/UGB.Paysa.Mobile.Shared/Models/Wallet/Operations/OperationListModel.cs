using System.ComponentModel;
using Abp.AutoMapper;
using UGB.Paysa.Authorization.Users.Dto;
using UGB.Paysa.Wallet.Operations.Dtos;
using Xamarin.Forms;

namespace UGB.Paysa.Models.Users
{
    [AutoMapFrom(typeof(GetOperationForViewDto))]
    public class OperationListModel : GetOperationForViewDto
    {
        public string Mois { get; set; }
        public string Icon { get; set; }
        public int Annee { get; set; }
        public double Montant => Operation.Montant;
        public string Signe => Operation.Discriminator == "Debit" ? "-" : "+";
    }
}