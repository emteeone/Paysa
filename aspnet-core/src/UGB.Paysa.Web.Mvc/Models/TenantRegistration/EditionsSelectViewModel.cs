using Abp.AutoMapper;
using UGB.Paysa.MultiTenancy.Dto;

namespace UGB.Paysa.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(EditionsSelectOutput))]
    public class EditionsSelectViewModel : EditionsSelectOutput
    {
    }
}
