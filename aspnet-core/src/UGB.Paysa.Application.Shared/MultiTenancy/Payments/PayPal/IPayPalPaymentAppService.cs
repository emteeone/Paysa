using System.Threading.Tasks;
using Abp.Application.Services;
using UGB.Paysa.MultiTenancy.Payments.PayPal.Dto;

namespace UGB.Paysa.MultiTenancy.Payments.PayPal
{
    public interface IPayPalPaymentAppService : IApplicationService
    {
        Task ConfirmPayment(long paymentId, string paypalOrderId);

        PayPalConfigurationDto GetConfiguration();
    }
}
