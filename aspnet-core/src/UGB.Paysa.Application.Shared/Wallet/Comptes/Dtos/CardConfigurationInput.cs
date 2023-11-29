using System;
using System.Collections.Generic;
using System.Text;

namespace UGB.Paysa.Wallet.Comptes.Dtos
{
    public class CardConfigurationInput
    {
        public string CodeEtudiant { get; set; }
        public string NumeroCarte { get; set; }
        public string NumeroCompte { get; set; }
        public string UID { get; set; }
    }
}
