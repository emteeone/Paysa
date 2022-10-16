using Abp.Application.Services.Dto;

namespace UGB.Paysa.Wallet.Tools.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}