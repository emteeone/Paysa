using UGB.Paysa.Wallet.Etudiants;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace UGB.Paysa.Wallet.Comptes
{
    [Table("UgbCompte")]
    [Audited]
    public class Compte : FullAuditedEntity<string>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        public virtual string NumeroCompte { get; set; }

        public virtual double Solde { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual DateTime DateCreation { get; set; }

        public virtual string EtudiantId { get; set; }

        [ForeignKey("EtudiantId")]
        public Etudiant EtudiantFk { get; set; }

    }
}