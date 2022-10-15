using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using UGB.Paysa.Authorization;
using UGB.Paysa.DashboardCustomization;
using System.Threading.Tasks;
using UGB.Paysa.Web.Areas.App.Startup;

namespace UGB.Paysa.Web.Areas.App.Controllers
{
    [Area("App")]
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Host_Dashboard)]
    public class HostDashboardController : CustomizableDashboardControllerBase
    {
        public HostDashboardController(
            DashboardViewConfiguration dashboardViewConfiguration,
            IDashboardCustomizationAppService dashboardCustomizationAppService)
            : base(dashboardViewConfiguration, dashboardCustomizationAppService)
        {

        }

        public async Task<ActionResult> Index()
        {
            return await GetView(PaysaDashboardCustomizationConsts.DashboardNames.DefaultHostDashboard);
        }
    }
}