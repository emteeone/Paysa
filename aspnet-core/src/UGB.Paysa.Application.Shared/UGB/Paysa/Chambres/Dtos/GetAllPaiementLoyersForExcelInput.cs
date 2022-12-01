using Abp.Application.Services.Dto;
using System;

namespace UGB.Paysa.UGB.Paysa.Chambres.Dtos
{
    public class GetAllPaiementLoyersForExcelInput
    {
        public string Filter { get; set; }

        public string MoisFilter { get; set; }

        public int? MaxAnneeFilter { get; set; }
        public int? MinAnneeFilter { get; set; }

        public string CodePaiementFilter { get; set; }

        public string ChambreReferenceFilter { get; set; }

        public string OperationCodeOperationFilter { get; set; }

    }
}