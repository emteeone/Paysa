using System.Threading.Tasks;
using Abp.Application.Services;
using UGB.Paysa.Configuration.Host.Dto;

namespace UGB.Paysa.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
