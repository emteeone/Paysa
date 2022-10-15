using System.Threading.Tasks;
using Abp.Application.Services;
using UGB.Paysa.Install.Dto;

namespace UGB.Paysa.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}