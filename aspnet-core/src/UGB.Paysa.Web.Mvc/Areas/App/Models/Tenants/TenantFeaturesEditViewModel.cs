using Abp.AutoMapper;
using UGB.Paysa.MultiTenancy;
using UGB.Paysa.MultiTenancy.Dto;
using UGB.Paysa.Web.Areas.App.Models.Common;

namespace UGB.Paysa.Web.Areas.App.Models.Tenants
{
    [AutoMapFrom(typeof (GetTenantFeaturesEditOutput))]
    public class TenantFeaturesEditViewModel : GetTenantFeaturesEditOutput, IFeatureEditViewModel
    {
        public Tenant Tenant { get; set; }
    }
}