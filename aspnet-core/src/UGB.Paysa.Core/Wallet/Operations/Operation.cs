using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UGB.Paysa.Wallet.Operations
{
    public abstract class Operation : FullAuditedEntity<string>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        public virtual string CodeOperation { get; set; }
        public virtual DateTime DateOperation { get; set; }
        [Required]
        public virtual double Montant { get; set; }

    }
}
