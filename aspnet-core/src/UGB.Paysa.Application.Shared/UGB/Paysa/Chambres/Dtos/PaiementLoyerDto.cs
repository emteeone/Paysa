using System;
using Abp.Application.Services.Dto;

namespace UGB.Paysa.UGB.Paysa.Chambres.Dtos
{
    public class PaiementLoyerDto : EntityDto<string>
    {
        public string Mois { get; set; }

        public int Annee { get; set; }

        public string CodePaiement { get; set; }

        public string ChambreId { get; set; }

        public string OperationId { get; set; }

    }
}