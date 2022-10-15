using System.Threading.Tasks;
using Abp.Dependency;

namespace UGB.Paysa.MultiTenancy.Accounting
{
    public interface IInvoiceNumberGenerator : ITransientDependency
    {
        Task<string> GetNewInvoiceNumber();
    }
}