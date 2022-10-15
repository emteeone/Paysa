using Abp.AutoMapper;
using UGB.Paysa.Authorization.Users;
using UGB.Paysa.Authorization.Users.Dto;
using UGB.Paysa.Web.Areas.App.Models.Common;

namespace UGB.Paysa.Web.Areas.App.Models.Users
{
    [AutoMapFrom(typeof(GetUserPermissionsForEditOutput))]
    public class UserPermissionsEditViewModel : GetUserPermissionsForEditOutput, IPermissionsEditViewModel
    {
        public User User { get; set; }
    }
}