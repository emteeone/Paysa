using UGB.Paysa.Wallet.Comptes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace UGB.Paysa.Wallet.Operations
{
    [Table("UgbOperations")]
    public class Operation : Entity<string>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        public virtual string CodeOperation { get; set; }

        [Required]
        public virtual DateTime DateOperation { get; set; }

        [Required]
        public virtual double Montant { get; set; }

        [Required]
        public virtual string Discriminator { get; set; }

        [Required]
        public virtual DateTime CreationTime { get; set; }

        public virtual DateTime? LastModificationTime { get; set; }

        [Required]
        public virtual bool IsDeleted { get; set; }

        public virtual DateTime? DeletionTime { get; set; }

        public virtual string? CompteId { get; set; }

        [ForeignKey("CompteId")]
        public Compte CompteFk { get; set; }

    }
}