using System;
using Abp.Application.Services.Dto;

namespace UGB.Paysa.Wallet.Comptes.Dtos
{
    public class CompteDto : EntityDto<string>
    {
        public string NumeroCompte { get; set; }

        public double Solde { get; set; }

        public bool IsActive { get; set; }

        public DateTime DateCreation { get; set; }

        public string EtudiantId { get; set; }

    }
}