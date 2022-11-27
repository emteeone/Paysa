using System;
using Abp.Application.Services.Dto;

namespace UGB.Paysa.Wallet.Operations.Dtos
{
    public class TypeOperationDto : EntityDto<string>
    {
        public string Reference { get; set; }

        public string Nom { get; set; }

        public double Prix { get; set; }

    }
}