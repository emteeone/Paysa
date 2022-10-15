using Abp.AutoMapper;
using UGB.Paysa.Organizations.Dto;

namespace UGB.Paysa.Models.Users
{
    [AutoMapFrom(typeof(OrganizationUnitDto))]
    public class OrganizationUnitModel : OrganizationUnitDto
    {
        public bool IsAssigned { get; set; }
    }
}