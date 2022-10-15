using System.Threading.Tasks;
using Abp.Authorization.Users;
using UGB.Paysa.Authorization.Users;

namespace UGB.Paysa.Authorization
{
    public static class UserManagerExtensions
    {
        public static async Task<User> GetAdminAsync(this UserManager userManager)
        {
            return await userManager.FindByNameAsync(AbpUserBase.AdminUserName);
        }
    }
}
