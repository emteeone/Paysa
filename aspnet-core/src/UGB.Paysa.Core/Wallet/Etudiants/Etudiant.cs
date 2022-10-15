using UGB.Paysa.Wallet.Enum;
using UGB.Paysa.Wallet.Chambres;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace UGB.Paysa.Wallet.Etudiants
{
    [Table("UgbEtudiant")]
    [Audited]
    public class Etudiant : FullAuditedEntity<string>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        public virtual string INE { get; set; }

        public virtual string CodeEtudiant { get; set; }

        [Required]
        public virtual string Prenom { get; set; }

        [Required]
        public virtual string Nom { get; set; }

        public virtual Sexe Sexe { get; set; }

        [Required]
        public virtual DateTime DateDeNaissance { get; set; }

        [Required]
        public virtual string LieuDeNaissance { get; set; }

        public virtual SituationMatrimoniale SituationMatrimoniale { get; set; }

        [Required]
        public virtual string CIN { get; set; }

        [Required]
        public virtual string Adresse { get; set; }

        [Required]
        public virtual string Ville { get; set; }

        [Required]
        public virtual string Region { get; set; }

        [Required]
        public virtual string Pays { get; set; }

        [Required]
        public virtual string Email { get; set; }

        [Required]
        public virtual string Telephone { get; set; }

        public virtual string ChambreId { get; set; }

        [ForeignKey("ChambreId")]
        public Chambre ChambreFk { get; set; }

    }
}