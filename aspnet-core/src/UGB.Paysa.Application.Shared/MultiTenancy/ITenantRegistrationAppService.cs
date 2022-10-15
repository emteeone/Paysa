using System.Threading.Tasks;
using Abp.Application.Services;
using UGB.Paysa.Editions.Dto;
using UGB.Paysa.MultiTenancy.Dto;

namespace UGB.Paysa.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}