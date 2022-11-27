using Abp.Application.Services.Dto;
using System;

namespace UGB.Paysa.Wallet.Operations.Dtos
{
    public class GetAllTypeOperationsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string ReferenceFilter { get; set; }

        public string NomFilter { get; set; }

        public double? MaxPrixFilter { get; set; }
        public double? MinPrixFilter { get; set; }

    }
}