using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace UGB.Paysa.Wallet.Tools.Dtos
{
    public class CreateOrEditCarteDto : EntityDto<string>
    {

        [Required]
        public string UID { get; set; }

        public string NumeroCarte { get; set; }

        public bool IsActive { get; set; }

        public DateTime DateDelivrance { get; set; }

        public DateTime DateExpiration { get; set; }

        public string CompteId { get; set; }

    }
}