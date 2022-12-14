using System;
using Abp.Application.Services.Dto;

namespace UGB.Paysa.Wallet.Tools.Dtos
{
    public class CarteDto : EntityDto<string>
    {
        public string UID { get; set; }

        public string NumeroCarte { get; set; }

        public bool IsActive { get; set; }

        public DateTime DateDelivrance { get; set; }

        public DateTime DateExpiration { get; set; }

        public string CompteId { get; set; }

    }
}