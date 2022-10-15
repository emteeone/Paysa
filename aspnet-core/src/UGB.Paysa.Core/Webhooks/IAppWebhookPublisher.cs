using System.Threading.Tasks;
using UGB.Paysa.Authorization.Users;

namespace UGB.Paysa.WebHooks
{
    public interface IAppWebhookPublisher
    {
        Task PublishTestWebhook();
    }
}
