using Abp.Modules;
using Abp.Reflection.Extensions;

namespace UGB.Paysa
{
    [DependsOn(typeof(PaysaXamarinSharedModule))]
    public class PaysaXamarinIosModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PaysaXamarinIosModule).GetAssembly());
        }
    }
}