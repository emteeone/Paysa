using Abp.Application.Services.Dto;
using System;

namespace UGB.Paysa.Wallet.Tools.Dtos
{
    public class GetAllTerminauxForExcelInput
    {
        public string Filter { get; set; }

        public string Uid_DeviceFilter { get; set; }

        public string MatriculeFilter { get; set; }

        public string PositionFilter { get; set; }

    }
}