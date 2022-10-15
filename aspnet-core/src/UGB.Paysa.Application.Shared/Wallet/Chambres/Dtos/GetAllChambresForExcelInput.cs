using Abp.Application.Services.Dto;
using System;

namespace UGB.Paysa.Wallet.Chambres.Dtos
{
    public class GetAllChambresForExcelInput
    {
        public string Filter { get; set; }

        public string ReferenceFilter { get; set; }

        public int? MaxNumeroChambreFilter { get; set; }
        public int? MinNumeroChambreFilter { get; set; }

        public int? BlocFilter { get; set; }

        public int? VillageFilter { get; set; }

        public int? CampusFilter { get; set; }

        public double? MaxMontantLocationFilter { get; set; }
        public double? MinMontantLocationFilter { get; set; }

    }
}