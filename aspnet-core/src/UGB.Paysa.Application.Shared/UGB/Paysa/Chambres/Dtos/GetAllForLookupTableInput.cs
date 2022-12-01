using Abp.Application.Services.Dto;

namespace UGB.Paysa.UGB.Paysa.Chambres.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}