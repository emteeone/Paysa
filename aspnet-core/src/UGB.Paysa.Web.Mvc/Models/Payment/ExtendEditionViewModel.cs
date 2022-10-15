using System.Collections.Generic;
using UGB.Paysa.Editions.Dto;
using UGB.Paysa.MultiTenancy.Payments;

namespace UGB.Paysa.Web.Models.Payment
{
    public class ExtendEditionViewModel
    {
        public EditionSelectDto Edition { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}