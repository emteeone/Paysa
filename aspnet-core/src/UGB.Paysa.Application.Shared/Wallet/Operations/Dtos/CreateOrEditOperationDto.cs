using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace UGB.Paysa.Wallet.Operations.Dtos
{
    public class CreateOrEditOperationDto : EntityDto<string>
    {

        [Required]
        public string CodeOperation { get; set; }

        [Required]
        public DateTime DateOperation { get; set; }

        [Required]
        public double Montant { get; set; }

        [Required]
        public string Discriminator { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public DateTime? DeletionTime { get; set; }

        public string CompteId { get; set; }

    }
}