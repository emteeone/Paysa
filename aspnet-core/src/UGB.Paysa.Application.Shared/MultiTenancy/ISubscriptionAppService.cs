using System.Threading.Tasks;
using Abp.Application.Services;

namespace UGB.Paysa.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task DisableRecurringPayments();

        Task EnableRecurringPayments();
    }
}
