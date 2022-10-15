using Abp.Authorization;
using UGB.Paysa.Authorization.Roles;
using UGB.Paysa.Authorization.Users;

namespace UGB.Paysa.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
