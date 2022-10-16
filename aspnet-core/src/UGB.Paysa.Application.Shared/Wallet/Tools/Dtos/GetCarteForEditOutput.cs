using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace UGB.Paysa.Wallet.Tools.Dtos
{
    public class GetCarteForEditOutput
    {
        public CreateOrEditCarteDto Carte { get; set; }

        public string CompteNumeroCompte { get; set; }

    }
}