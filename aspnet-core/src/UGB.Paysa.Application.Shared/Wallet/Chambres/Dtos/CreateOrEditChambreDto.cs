using UGB.Paysa.Wallet.Enum;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace UGB.Paysa.Wallet.Chambres.Dtos
{
    public class CreateOrEditChambreDto : EntityDto<string>
    {

        [Required]
        public string Reference { get; set; }

        public int NumeroChambre { get; set; }

        public Bloc Bloc { get; set; }

        public Village Village { get; set; }

        public Campus Campus { get; set; }

        [Required]
        public double MontantLocation { get; set; }

    }
}