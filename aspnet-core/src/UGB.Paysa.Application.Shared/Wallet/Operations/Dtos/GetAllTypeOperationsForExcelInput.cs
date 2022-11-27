using Abp.Application.Services.Dto;
using System;

namespace UGB.Paysa.Wallet.Operations.Dtos
{
    public class GetAllTypeOperationsForExcelInput
    {
        public string Filter { get; set; }

        public string ReferenceFilter { get; set; }

        public string NomFilter { get; set; }

        public double? MaxPrixFilter { get; set; }
        public double? MinPrixFilter { get; set; }

    }
}