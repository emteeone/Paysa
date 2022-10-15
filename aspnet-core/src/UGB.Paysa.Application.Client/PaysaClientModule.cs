using Abp.Modules;
using Abp.Reflection.Extensions;

namespace UGB.Paysa
{
    public class PaysaClientModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PaysaClientModule).GetAssembly());
        }
    }
}
