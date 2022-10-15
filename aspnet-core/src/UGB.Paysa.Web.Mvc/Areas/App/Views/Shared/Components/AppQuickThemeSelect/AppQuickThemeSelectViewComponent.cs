using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UGB.Paysa.Web.Areas.App.Models.Layout;
using UGB.Paysa.Web.Views;

namespace UGB.Paysa.Web.Areas.App.Views.Shared.Components.
    AppQuickThemeSelect
{
    public class AppQuickThemeSelectViewComponent : PaysaViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(string cssClass)
        {
            return Task.FromResult<IViewComponentResult>(View(new QuickThemeSelectionViewModel
            {
                CssClass = cssClass
            }));
        }
    }
}
