using Abp.Application.Services.Dto;

namespace UGB.Paysa.Wallet.Etudiants.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}