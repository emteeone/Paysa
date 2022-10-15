using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace UGB.Paysa.Startup
{
    [DependsOn(typeof(PaysaCoreModule))]
    public class PaysaGraphQLModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PaysaGraphQLModule).GetAssembly());
        }

        public override void PreInitialize()
        {
            base.PreInitialize();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }
    }
}