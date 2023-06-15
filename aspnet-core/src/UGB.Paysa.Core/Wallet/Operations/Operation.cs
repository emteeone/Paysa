using UGB.Paysa.Wallet.Comptes;
using UGB.Paysa.Wallet.Operations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using UGB.Paysa.Wallet.Tools;

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

        public virtual string CompteId { get; set; }

        [ForeignKey("CompteId")]
        public Compte CompteFk { get; set; }

        public virtual string TypeOperationId { get; set; }

        [ForeignKey("TypeOperationId")]
        public TypeOperation TypeOperationFk { get; set; }

        public virtual string CarteId { get; set; }

        [ForeignKey("CarteId")]
        public Carte CarteFk { get; set; }
        public virtual string TerminalId { get; set; }

        [ForeignKey("TerminalId")]
        public Terminal TerminalFk { get; set; }

    }
}