using Abp.Application.Services.Dto;
using System;

namespace UGB.Paysa.Wallet.Etudiants.Dtos
{
    public class GetAllEtudiantsForExcelInput
    {
        public string Filter { get; set; }

        public string INEFilter { get; set; }

        public string CodeEtudiantFilter { get; set; }

        public string PrenomFilter { get; set; }

        public string NomFilter { get; set; }

        public int? SexeFilter { get; set; }

        public DateTime? MaxDateDeNaissanceFilter { get; set; }
        public DateTime? MinDateDeNaissanceFilter { get; set; }

        public string LieuDeNaissanceFilter { get; set; }

        public int? SituationMatrimonialeFilter { get; set; }

        public string CINFilter { get; set; }

        public string AdresseFilter { get; set; }

        public string VilleFilter { get; set; }

        public string RegionFilter { get; set; }

        public string PaysFilter { get; set; }

        public string EmailFilter { get; set; }

        public string TelephoneFilter { get; set; }

        public string ChambreReferenceFilter { get; set; }

    }
}