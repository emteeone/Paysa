using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace UGB.Paysa.Wallet.Operations
{
    [Table("UgbTypeOperation")]
    [Audited]
    public class TypeOperation : FullAuditedEntity<string>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        public virtual string Reference { get; set; }

        public virtual string Nom { get; set; }

        [Required]
        public virtual double Prix { get; set; }

    }
}