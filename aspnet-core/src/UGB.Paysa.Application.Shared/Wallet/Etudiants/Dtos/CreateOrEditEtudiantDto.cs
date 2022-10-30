using UGB.Paysa.Wallet.Enum;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace UGB.Paysa.Wallet.Etudiants.Dtos
{
    public class CreateOrEditEtudiantDto : EntityDto<string>
    {

        [Required]
        public string INE { get; set; }

        public string CodeEtudiant { get; set; }

        [Required]
        public string Prenom { get; set; }

        [Required]
        public string Nom { get; set; }

        public Sexe Sexe { get; set; }

        [Required]
        public DateTime DateDeNaissance { get; set; }

        [Required]
        public string LieuDeNaissance { get; set; }

        public SituationMatrimoniale SituationMatrimoniale { get; set; }

        [Required]
        public string CIN { get; set; }

        [Required]
        public string Adresse { get; set; }

        [Required]
        public string Ville { get; set; }

        [Required]
        public string Region { get; set; }

        [Required]
        public string Pays { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Telephone { get; set; }

        public string ChambreId { get; set; }

        public long? UserId { get; set; }

    }
}