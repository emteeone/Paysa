using System;
using System.Collections.Generic;
using System.Text;

namespace UGB.Paysa.Wallet.Comptes.Dtos
{
    public class CardConfigurationInfoDto
    {
        public bool CarteStatus { get; set; }
        public DateTime? CarteDateExpiration { get; set; }
        public string CompteNumero { get; set; }
        public string CarteNumero { get; set; }
        public double CompteSolde { get; set; }
        public string EtudiantNom { get; set; }
        public string EtudiantPrenom { get; set; }
    }
}
