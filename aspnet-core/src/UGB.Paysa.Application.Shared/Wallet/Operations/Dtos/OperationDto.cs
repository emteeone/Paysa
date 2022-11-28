using System;
using Abp.Application.Services.Dto;

namespace UGB.Paysa.Wallet.Operations.Dtos
{
    public class OperationDto : EntityDto<string>
    {
        public string CodeOperation { get; set; }

        public DateTime DateOperation { get; set; }

        public double Montant { get; set; }

        public string Discriminator { get; set; }

        public string CompteId { get; set; }

        public string TypeProductionId { get; set; }

    }
}