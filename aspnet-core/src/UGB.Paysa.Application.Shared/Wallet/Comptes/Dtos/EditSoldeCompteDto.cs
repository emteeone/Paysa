using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace UGB.Paysa.Wallet.Comptes.Dtos
{
    public class EditSoldeCompteDto
    {

        public string NumeroCompte { get; set; }
        public string Id { get; set; }

        [Required]
        public double Montant { get; set; }

    }
}