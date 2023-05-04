using System;

namespace UGB.Paysa.UGB.Paysa.Chambres.Dtos
{
    public class GetPaiementLoyerForViewDto
    {
        public PaiementLoyerDto PaiementLoyer { get; set; }

        public string ChambreReference { get; set; }

        public string OperationCodeOperation { get; set; }
        public DateTime OperationDateOperation { get; set; }

    }
}