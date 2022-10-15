using Abp.Application.Services.Dto;

namespace UGB.Paysa.Wallet.Chambres.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}