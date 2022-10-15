using UGB.Paysa.Wallet.Enum;

using System;
using Abp.Application.Services.Dto;

namespace UGB.Paysa.Wallet.Chambres.Dtos
{
    public class ChambreDto : EntityDto<string>
    {
        public string Reference { get; set; }

        public int NumeroChambre { get; set; }

        public Bloc Bloc { get; set; }

        public Village Village { get; set; }

        public Campus Campus { get; set; }

        public double MontantLocation { get; set; }

    }
}