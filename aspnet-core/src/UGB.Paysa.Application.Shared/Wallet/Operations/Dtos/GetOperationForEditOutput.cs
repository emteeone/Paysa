using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace UGB.Paysa.Wallet.Operations.Dtos
{
    public class GetOperationForEditOutput
    {
        public CreateOrEditOperationDto Operation { get; set; }

        public string CompteNumeroCompte { get; set; }

    }
}