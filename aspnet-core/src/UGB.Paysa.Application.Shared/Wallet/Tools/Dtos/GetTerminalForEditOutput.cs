using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace UGB.Paysa.Wallet.Tools.Dtos
{
    public class GetTerminalForEditOutput
    {
        public CreateOrEditTerminalDto Terminal { get; set; }

    }
}