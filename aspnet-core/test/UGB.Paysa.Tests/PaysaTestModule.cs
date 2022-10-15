using Abp.Modules;
using UGB.Paysa.Test.Base;

namespace UGB.Paysa.Tests
{
    [DependsOn(typeof(PaysaTestBaseModule))]
    public class PaysaTestModule : AbpModule
    {
       
    }
}
