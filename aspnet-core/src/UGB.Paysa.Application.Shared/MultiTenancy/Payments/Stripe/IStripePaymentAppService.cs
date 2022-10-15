using System.Threading.Tasks;
using Abp.Application.Services;
using UGB.Paysa.MultiTenancy.Payments.Dto;
using UGB.Paysa.MultiTenancy.Payments.Stripe.Dto;

namespace UGB.Paysa.MultiTenancy.Payments.Stripe
{
    public interface IStripePaymentAppService : IApplicationService
    {
        Task ConfirmPayment(StripeConfirmPaymentInput input);

        StripeConfigurationDto GetConfiguration();

        Task<SubscriptionPaymentDto> GetPaymentAsync(StripeGetPaymentInput input);

        Task<string> CreatePaymentSession(StripeCreatePaymentSessionInput input);
    }
}