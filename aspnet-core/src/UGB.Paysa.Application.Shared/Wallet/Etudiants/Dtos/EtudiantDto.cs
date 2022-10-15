using UGB.Paysa.Wallet.Enum;

using System;
using Abp.Application.Services.Dto;

namespace UGB.Paysa.Wallet.Etudiants.Dtos
{
    public class EtudiantDto : EntityDto<string>
    {
        public string INE { get; set; }

        public string CodeEtudiant { get; set; }

        public string Prenom { get; set; }

        public string Nom { get; set; }

        public Sexe Sexe { get; set; }

        public DateTime DateDeNaissance { get; set; }

        public string LieuDeNaissance { get; set; }

        public SituationMatrimoniale SituationMatrimoniale { get; set; }

        public string CIN { get; set; }

        public string Adresse { get; set; }

        public string Ville { get; set; }

        public string Region { get; set; }

        public string Pays { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public string ChambreId { get; set; }

    }
}