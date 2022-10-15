using Abp.Application.Services.Dto;
using Abp.Webhooks;
using UGB.Paysa.WebHooks.Dto;

namespace UGB.Paysa.Web.Areas.App.Models.Webhooks
{
    public class CreateOrEditWebhookSubscriptionViewModel
    {
        public WebhookSubscription WebhookSubscription { get; set; }

        public ListResultDto<GetAllAvailableWebhooksOutput> AvailableWebhookEvents { get; set; }
    }
}
