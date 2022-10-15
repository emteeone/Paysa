using System.Collections.Generic;
using UGB.Paysa.Editions;
using UGB.Paysa.Editions.Dto;
using UGB.Paysa.MultiTenancy.Payments;
using UGB.Paysa.MultiTenancy.Payments.Dto;

namespace UGB.Paysa.Web.Models.Payment
{
    public class BuyEditionViewModel
    {
        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public decimal? AdditionalPrice { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}
