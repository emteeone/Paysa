using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace UGB.Paysa.Web.Public.Views
{
    public abstract class PaysaRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected PaysaRazorPage()
        {
            LocalizationSourceName = PaysaConsts.LocalizationSourceName;
        }
    }
}
