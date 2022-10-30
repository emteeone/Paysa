using Abp.Application.Services.Dto;
using System;

namespace UGB.Paysa.Wallet.Operations.Dtos
{
    public class GetAllOperationsForExcelInput
    {
        public string Filter { get; set; }

        public string CodeOperationFilter { get; set; }

        public DateTime? MaxDateOperationFilter { get; set; }
        public DateTime? MinDateOperationFilter { get; set; }

        public double? MaxMontantFilter { get; set; }
        public double? MinMontantFilter { get; set; }

        public string DiscriminatorFilter { get; set; }

        public DateTime? MaxCreationTimeFilter { get; set; }
        public DateTime? MinCreationTimeFilter { get; set; }

        public DateTime? MaxLastModificationTimeFilter { get; set; }
        public DateTime? MinLastModificationTimeFilter { get; set; }

        public int? IsDeletedFilter { get; set; }

        public DateTime? MaxDeletionTimeFilter { get; set; }
        public DateTime? MinDeletionTimeFilter { get; set; }

        public string CompteNumeroCompteFilter { get; set; }

    }
}