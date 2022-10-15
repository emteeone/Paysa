using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace UGB.Paysa.Wallet.Comptes.Dtos
{
    public class GetCompteForEditOutput
    {
        public CreateOrEditCompteDto Compte { get; set; }

        public string EtudiantCodeEtudiant { get; set; }

    }
}