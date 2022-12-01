using UGB.Paysa.Wallet.Chambres;
using UGB.Paysa.Wallet.Operations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace UGB.Paysa.UGB.Paysa.Chambres
{
    [Table("UgbPaiementLoyer")]
    [Audited]
    public class PaiementLoyer : FullAuditedEntity<string>, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        public virtual string Mois { get; set; }

        [Required]
        public virtual int Annee { get; set; }

        [Required]
        public virtual string CodePaiement { get; set; }

        public virtual string ChambreId { get; set; }

        [ForeignKey("ChambreId")]
        public Chambre ChambreFk { get; set; }

        public virtual string OperationId { get; set; }

        [ForeignKey("OperationId")]
        public Operation OperationFk { get; set; }

    }
}