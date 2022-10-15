using Microsoft.Extensions.Configuration;

namespace UGB.Paysa.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
