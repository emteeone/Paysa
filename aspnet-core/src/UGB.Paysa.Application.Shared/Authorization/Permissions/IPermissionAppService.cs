using Abp.Application.Services;
using Abp.Application.Services.Dto;
using UGB.Paysa.Authorization.Permissions.Dto;

namespace UGB.Paysa.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
