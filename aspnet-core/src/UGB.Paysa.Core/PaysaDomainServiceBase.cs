using Abp.Domain.Services;

namespace UGB.Paysa
{
    public abstract class PaysaDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected PaysaDomainServiceBase()
        {
            LocalizationSourceName = PaysaConsts.LocalizationSourceName;
        }
    }
}
