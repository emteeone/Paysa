using System.Threading.Tasks;
using Abp.Webhooks;

namespace UGB.Paysa.WebHooks
{
    public interface IWebhookEventAppService
    {
        Task<WebhookEvent> Get(string id);
    }
}
