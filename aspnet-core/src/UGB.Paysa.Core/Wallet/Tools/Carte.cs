using UGB.Paysa.Wallet.Comptes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace UGB.Paysa.Wallet.Tools
{
    [Table("UgbCarte")]
    [Audited]
    public class Carte : FullAuditedEntity<string>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        public virtual string UID { get; set; }

        public virtual string NumeroCarte { get; set; }

        public virtual bool IsActive { get; set; }
        public virtual bool IsBlocked { get; set; }
        public virtual bool IsPersonnalized { get; set; }

        public virtual DateTime DateDelivrance { get; set; }

        public virtual DateTime DateExpiration { get; set; }

        public virtual string CompteId { get; set; }

        [ForeignKey("CompteId")]
        public Compte CompteFk { get; set; }

    }
}