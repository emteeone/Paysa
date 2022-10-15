using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UGB.Paysa.Common.Dto;
using UGB.Paysa.Editions.Dto;

namespace UGB.Paysa.Common
{
    public interface ICommonLookupAppService : IApplicationService
    {
        Task<ListResultDto<SubscribableEditionComboboxItemDto>> GetEditionsForCombobox(bool onlyFreeItems = false);

        Task<PagedResultDto<NameValueDto>> FindUsers(FindUsersInput input);

        GetDefaultEditionNameOutput GetDefaultEditionName();
    }
}