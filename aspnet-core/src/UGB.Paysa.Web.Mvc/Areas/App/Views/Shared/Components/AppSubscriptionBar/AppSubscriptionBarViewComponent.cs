using System.Linq;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using UGB.Paysa.Authorization;
using UGB.Paysa.Configuration;
using UGB.Paysa.Web.Areas.App.Models.Layout;
using UGB.Paysa.Web.Session;
using UGB.Paysa.Web.Views;

namespace UGB.Paysa.Web.Areas.App.Views.Shared.Components.AppSubscriptionBar
{
    public class AppSubscriptionBarViewComponent : PaysaViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppSubscriptionBarViewComponent(
            IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var headerModel = new HeaderViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync(),
                SubscriptionExpireNotifyDayCount = SettingManager.GetSettingValue<int>(AppSettings.TenantManagement.SubscriptionExpireNotifyDayCount)
            };

            return View(headerModel);
        }

    }
}
