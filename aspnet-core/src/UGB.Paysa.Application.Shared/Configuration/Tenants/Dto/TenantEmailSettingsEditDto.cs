using Abp.Auditing;
using UGB.Paysa.Configuration.Dto;

namespace UGB.Paysa.Configuration.Tenants.Dto
{
    public class TenantEmailSettingsEditDto : EmailSettingsEditDto
    {
        public bool UseHostDefaultEmailSettings { get; set; }
    }
}