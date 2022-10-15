using System.Collections.Generic;
using Abp.Application.Services.Dto;
using UGB.Paysa.Authorization.Permissions.Dto;
using UGB.Paysa.Web.Areas.App.Models.Common;

namespace UGB.Paysa.Web.Areas.App.Models.Roles
{
    public class RoleListViewModel : IPermissionsEditViewModel
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}