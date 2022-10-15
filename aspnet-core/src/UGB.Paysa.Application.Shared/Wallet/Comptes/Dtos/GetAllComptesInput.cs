using Abp.Application.Services.Dto;
using System;

namespace UGB.Paysa.Wallet.Comptes.Dtos
{
    public class GetAllComptesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NumeroCompteFilter { get; set; }

        public double? MaxSoldeFilter { get; set; }
        public double? MinSoldeFilter { get; set; }

        public int? IsActiveFilter { get; set; }

        public DateTime? MaxDateCreationFilter { get; set; }
        public DateTime? MinDateCreationFilter { get; set; }

        public string EtudiantCodeEtudiantFilter { get; set; }

    }
}