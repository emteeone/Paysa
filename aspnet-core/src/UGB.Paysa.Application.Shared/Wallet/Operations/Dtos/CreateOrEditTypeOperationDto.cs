using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace UGB.Paysa.Wallet.Operations.Dtos
{
    public class CreateOrEditTypeOperationDto : EntityDto<string>
    {

        [Required]
        public string Reference { get; set; }

        public string Nom { get; set; }

        [Required]
        public double Prix { get; set; }

    }
}