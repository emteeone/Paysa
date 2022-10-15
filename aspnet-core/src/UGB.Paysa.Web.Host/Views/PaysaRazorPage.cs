using Abp.AspNetCore.Mvc.Views;

namespace UGB.Paysa.Web.Views
{
    public abstract class PaysaRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected PaysaRazorPage()
        {
            LocalizationSourceName = PaysaConsts.LocalizationSourceName;
        }
    }
}
