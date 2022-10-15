using Abp.Modules;
using Abp.Reflection.Extensions;

namespace UGB.Paysa
{
    [DependsOn(typeof(PaysaXamarinSharedModule))]
    public class PaysaXamarinAndroidModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PaysaXamarinAndroidModule).GetAssembly());
        }
    }
}