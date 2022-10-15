using System.Threading.Tasks;
using Abp.Application.Services;
using UGB.Paysa.Configuration.Tenants.Dto;

namespace UGB.Paysa.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
