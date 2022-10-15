using Abp.AutoMapper;
using UGB.Paysa.Sessions.Dto;

namespace UGB.Paysa.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}