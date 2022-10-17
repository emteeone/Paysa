using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace UGB.Paysa.Wallet.Tools.Dtos
{
    public class CreateOrEditTerminalDto : EntityDto<string>
    {

        [Required]
        public string Uid_Device { get; set; }

        [Required]
        public string Matricule { get; set; }

        [Required]
        public string Position { get; set; }

    }
}