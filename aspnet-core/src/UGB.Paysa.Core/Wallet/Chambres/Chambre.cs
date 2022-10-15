using UGB.Paysa.Wallet.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace UGB.Paysa.Wallet.Chambres
{
    [Table("UgbChambre")]
    [Audited]
    public class Chambre : FullAuditedEntity<string>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        public virtual string Reference { get; set; }

        public virtual int NumeroChambre { get; set; }

        public virtual Bloc Bloc { get; set; }

        public virtual Village Village { get; set; }

        public virtual Campus Campus { get; set; }

        [Required]
        public virtual double MontantLocation { get; set; }

    }
}