using Abp.Application.Services.Dto;

namespace UGB.Paysa.Wallet.Comptes.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}