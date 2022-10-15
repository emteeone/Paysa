using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace UGB.Paysa.Wallet.Chambres.Dtos
{
    public class GetChambreForEditOutput
    {
        public CreateOrEditChambreDto Chambre { get; set; }

    }
}