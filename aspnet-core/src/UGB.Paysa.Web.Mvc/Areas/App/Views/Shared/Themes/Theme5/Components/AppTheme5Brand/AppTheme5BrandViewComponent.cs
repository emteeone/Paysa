using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UGB.Paysa.Web.Areas.App.Models.Layout;
using UGB.Paysa.Web.Session;
using UGB.Paysa.Web.Views;

namespace UGB.Paysa.Web.Areas.App.Views.Shared.Themes.Theme5.Components.AppTheme5Brand
{
    public class AppTheme5BrandViewComponent : PaysaViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppTheme5BrandViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var headerModel = new HeaderViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(headerModel);
        }
    }
}
