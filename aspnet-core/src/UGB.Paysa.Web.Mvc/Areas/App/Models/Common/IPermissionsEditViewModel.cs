using System.Collections.Generic;
using UGB.Paysa.Authorization.Permissions.Dto;

namespace UGB.Paysa.Web.Areas.App.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }

        List<string> GrantedPermissionNames { get; set; }
    }
}