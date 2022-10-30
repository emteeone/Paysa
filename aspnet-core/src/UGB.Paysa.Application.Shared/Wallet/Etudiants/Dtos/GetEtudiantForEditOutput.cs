using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace UGB.Paysa.Wallet.Etudiants.Dtos
{
    public class GetEtudiantForEditOutput
    {
        public CreateOrEditEtudiantDto Etudiant { get; set; }

        public string ChambreReference { get; set; }

        public string UserName { get; set; }

    }
}