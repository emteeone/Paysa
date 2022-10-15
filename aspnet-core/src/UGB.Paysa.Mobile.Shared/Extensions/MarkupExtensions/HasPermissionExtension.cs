using System;
using UGB.Paysa.Core;
using UGB.Paysa.Core.Dependency;
using UGB.Paysa.Services.Permission;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UGB.Paysa.Extensions.MarkupExtensions
{
    [ContentProperty("Text")]
    public class HasPermissionExtension : IMarkupExtension
    {
        public string Text { get; set; }
        
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (ApplicationBootstrapper.AbpBootstrapper == null || Text == null)
            {
                return false;
            }

            var permissionService = DependencyResolver.Resolve<IPermissionService>();
            return permissionService.HasPermission(Text);
        }
    }
}