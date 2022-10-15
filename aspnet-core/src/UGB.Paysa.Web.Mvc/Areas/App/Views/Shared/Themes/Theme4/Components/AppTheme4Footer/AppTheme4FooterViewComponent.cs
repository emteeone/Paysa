using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UGB.Paysa.Web.Areas.App.Models.Layout;
using UGB.Paysa.Web.Session;
using UGB.Paysa.Web.Views;

namespace UGB.Paysa.Web.Areas.App.Views.Shared.Themes.Theme4.Components.AppTheme4Footer
{
    public class AppTheme4FooterViewComponent : PaysaViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppTheme4FooterViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var footerModel = new FooterViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(footerModel);
        }
    }
}
