using Abp.Application.Services.Dto;

namespace UGB.Paysa.Wallet.Operations.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}