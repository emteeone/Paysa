using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using UGB.Paysa.Authorization;

namespace UGB.Paysa
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(PaysaApplicationSharedModule),
        typeof(PaysaCoreModule)
        )]
    public class PaysaApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PaysaApplicationModule).GetAssembly());
        }
    }
}