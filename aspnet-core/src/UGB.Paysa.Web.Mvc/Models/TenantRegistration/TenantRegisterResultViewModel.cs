using Abp.AutoMapper;
using UGB.Paysa.MultiTenancy.Dto;

namespace UGB.Paysa.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(RegisterTenantOutput))]
    public class TenantRegisterResultViewModel : RegisterTenantOutput
    {
        public string TenantLoginAddress { get; set; }
    }
}