using Abp.Application.Services.Dto;
using System;

namespace UGB.Paysa.Wallet.Tools.Dtos
{
    public class GetAllCartesForExcelInput
    {
        public string Filter { get; set; }

        public string UIDFilter { get; set; }

        public string NumeroCarteFilter { get; set; }

        public int? IsActiveFilter { get; set; }

        public DateTime? MaxDateDelivranceFilter { get; set; }
        public DateTime? MinDateDelivranceFilter { get; set; }

        public DateTime? MaxDateExpirationFilter { get; set; }
        public DateTime? MinDateExpirationFilter { get; set; }

        public string CompteNumeroCompteFilter { get; set; }

    }
}