using Abp.AspNetCore.Mvc.ViewComponents;

namespace UGB.Paysa.Web.Views
{
    public abstract class PaysaViewComponent : AbpViewComponent
    {
        protected PaysaViewComponent()
        {
            LocalizationSourceName = PaysaConsts.LocalizationSourceName;
        }
    }
}