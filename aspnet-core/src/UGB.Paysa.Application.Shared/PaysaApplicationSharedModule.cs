using Abp.Modules;
using Abp.Reflection.Extensions;

namespace UGB.Paysa
{
    [DependsOn(typeof(PaysaCoreSharedModule))]
    public class PaysaApplicationSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PaysaApplicationSharedModule).GetAssembly());
        }
    }
}