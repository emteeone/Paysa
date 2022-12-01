using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace UGB.Paysa.UGB.Paysa.Chambres.Dtos
{
    public class GetPaiementLoyerForEditOutput
    {
        public CreateOrEditPaiementLoyerDto PaiementLoyer { get; set; }

        public string ChambreReference { get; set; }

        public string OperationCodeOperation { get; set; }

    }
}