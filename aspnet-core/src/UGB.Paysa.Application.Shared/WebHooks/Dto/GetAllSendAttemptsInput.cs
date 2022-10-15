using UGB.Paysa.Dto;

namespace UGB.Paysa.WebHooks.Dto
{
    public class GetAllSendAttemptsInput : PagedInputDto
    {
        public string SubscriptionId { get; set; }
    }
}
