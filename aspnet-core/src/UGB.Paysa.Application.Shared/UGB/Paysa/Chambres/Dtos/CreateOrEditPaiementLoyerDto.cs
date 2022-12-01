using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace UGB.Paysa.UGB.Paysa.Chambres.Dtos
{
    public class CreateOrEditPaiementLoyerDto : EntityDto<string>
    {

        [Required]
        public string Mois { get; set; }

        [Required]
        public int Annee { get; set; }

        [Required]
        public string CodePaiement { get; set; }

        public string ChambreId { get; set; }

        public string OperationId { get; set; }

    }
}