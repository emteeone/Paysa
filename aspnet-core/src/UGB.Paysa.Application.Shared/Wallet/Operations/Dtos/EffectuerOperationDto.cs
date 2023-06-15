using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace UGB.Paysa.Wallet.Operations.Dtos
{
    public class EffectuerOperationDto : EntityDto<string>
    {
        public string NumeroCompte { get; set; }
        [Required]
        public string NumeroCarte { get; set; }
        public string Terminal { get; set; }
        public string CodePin { get; set; }
        public DateTime DateOperation { get; set; }
        public double Montant { get; set; }
        [Required]
        public string TypeOperationReference { get; set; }
        public string Discriminator { get; set; }
        [Required]
        public string CodeEtudiant { get; set; }
    }
}