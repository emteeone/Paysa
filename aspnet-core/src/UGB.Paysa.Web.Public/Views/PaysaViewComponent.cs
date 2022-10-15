using Abp.AspNetCore.Mvc.ViewComponents;

namespace UGB.Paysa.Web.Public.Views
{
    public abstract class PaysaViewComponent : AbpViewComponent
    {
        protected PaysaViewComponent()
        {
            LocalizationSourceName = PaysaConsts.LocalizationSourceName;
        }
    }
}