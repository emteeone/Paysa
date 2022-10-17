using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace UGB.Paysa.Wallet.Tools
{
    [Table("UgbTerminal")]
    [Audited]
    public class Terminal : FullAuditedEntity<string>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        public virtual string Uid_Device { get; set; }

        [Required]
        public virtual string Matricule { get; set; }

        [Required]
        public virtual string Position { get; set; }

    }
}