using UGB.Paysa.Editions;
using UGB.Paysa.Editions.Dto;
using UGB.Paysa.MultiTenancy.Payments;
using UGB.Paysa.Security;
using UGB.Paysa.MultiTenancy.Payments.Dto;

namespace UGB.Paysa.Web.Models.TenantRegistration
{
    public class TenantRegisterViewModel
    {
        public PasswordComplexitySetting PasswordComplexitySetting { get; set; }

        public int? EditionId { get; set; }

        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }
    }
}
