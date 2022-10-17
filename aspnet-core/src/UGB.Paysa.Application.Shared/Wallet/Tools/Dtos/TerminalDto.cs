using System;
using Abp.Application.Services.Dto;

namespace UGB.Paysa.Wallet.Tools.Dtos
{
    public class TerminalDto : EntityDto<string>
    {
        public string Uid_Device { get; set; }

        public string Matricule { get; set; }

        public string Position { get; set; }

    }
}