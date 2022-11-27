using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace UGB.Paysa.Wallet.Operations.Dtos
{
    public class GetTypeOperationForEditOutput
    {
        public CreateOrEditTypeOperationDto TypeOperation { get; set; }

    }
}