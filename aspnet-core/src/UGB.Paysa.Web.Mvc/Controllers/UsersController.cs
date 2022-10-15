using Abp.AspNetCore.Mvc.Authorization;
using UGB.Paysa.Authorization;
using UGB.Paysa.Storage;
using Abp.BackgroundJobs;
using Abp.Authorization;

namespace UGB.Paysa.Web.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users)]
    public class UsersController : UsersControllerBase
    {
        public UsersController(IBinaryObjectManager binaryObjectManager, IBackgroundJobManager backgroundJobManager)
            : base(binaryObjectManager, backgroundJobManager)
        {
        }
    }
}