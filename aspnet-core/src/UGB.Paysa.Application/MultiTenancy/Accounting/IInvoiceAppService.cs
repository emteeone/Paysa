using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using UGB.Paysa.MultiTenancy.Accounting.Dto;

namespace UGB.Paysa.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}
