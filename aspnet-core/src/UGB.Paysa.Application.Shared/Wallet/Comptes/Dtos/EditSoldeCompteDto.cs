using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace UGB.Paysa.Wallet.Comptes.Dtos
{
    public class EditSoldeCompteDto
    {

        [Required]
        public string NumeroCompte { get; set; }

        [Required]
        public double Montant { get; set; }

    }
}