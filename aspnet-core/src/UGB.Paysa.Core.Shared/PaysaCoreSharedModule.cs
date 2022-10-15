using Abp.Modules;
using Abp.Reflection.Extensions;

namespace UGB.Paysa
{
    public class PaysaCoreSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PaysaCoreSharedModule).GetAssembly());
        }
    }
}