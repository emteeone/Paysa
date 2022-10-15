using UGB.Paysa.Editions.Dto;

namespace UGB.Paysa.MultiTenancy.Payments.Dto
{
    public class PaymentInfoDto
    {
        public EditionSelectDto Edition { get; set; }

        public decimal AdditionalPrice { get; set; }

        public bool IsLessThanMinimumUpgradePaymentAmount()
        {
            return AdditionalPrice < PaysaConsts.MinimumUpgradePaymentAmount;
        }
    }
}
