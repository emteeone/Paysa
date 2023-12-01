using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace UGB.Paysa.Wallet.Operations.Dtos
{
    public class EffectuerOperationOutputDto : EntityDto<string>
    {
        public DateTime DateOperation { get; set; }
        public double Montant { get; set; }
        public string TransactionId { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
    }
}