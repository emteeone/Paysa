using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace UGB.Paysa.Wallet.Operations.Dtos
{
    public class CreateOrEditOperationDto : EntityDto<string>
    {

        [Required]
        public string CodeOperation { get; set; }

        public DateTime DateOperation { get; set; }
        public double Montant { get; set; }

        public string Discriminator { get; set; }
        [Required]
        public string CompteId { get; set; }
        [Required]
        public string TypeProductionId { get; set; }

    }
}