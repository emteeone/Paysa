using Abp.Application.Services;
using UGB.Paysa.Dto;
using UGB.Paysa.Logging.Dto;

namespace UGB.Paysa.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
