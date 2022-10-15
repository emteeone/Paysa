using Abp.AutoMapper;
using UGB.Paysa.Authorization.Roles.Dto;
using UGB.Paysa.Web.Areas.App.Models.Common;

namespace UGB.Paysa.Web.Areas.App.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class CreateOrEditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool IsEditMode => Role.Id.HasValue;
    }
}