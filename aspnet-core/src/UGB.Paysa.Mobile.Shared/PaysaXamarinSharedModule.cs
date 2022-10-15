using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace UGB.Paysa
{
    [DependsOn(typeof(PaysaClientModule), typeof(AbpAutoMapperModule))]
    public class PaysaXamarinSharedModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.IsEnabled = false;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PaysaXamarinSharedModule).GetAssembly());
        }
    }
}