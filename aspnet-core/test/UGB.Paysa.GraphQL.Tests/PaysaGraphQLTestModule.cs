using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using UGB.Paysa.Configure;
using UGB.Paysa.Startup;
using UGB.Paysa.Test.Base;

namespace UGB.Paysa.GraphQL.Tests
{
    [DependsOn(
        typeof(PaysaGraphQLModule),
        typeof(PaysaTestBaseModule))]
    public class PaysaGraphQLTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.AddAndConfigureGraphQL();

            WindsorRegistrationHelper.CreateServiceProvider(IocManager.IocContainer, services);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PaysaGraphQLTestModule).GetAssembly());
        }
    }
}