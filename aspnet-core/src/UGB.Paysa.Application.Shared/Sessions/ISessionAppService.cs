using System.Threading.Tasks;
using Abp.Application.Services;
using UGB.Paysa.Sessions.Dto;

namespace UGB.Paysa.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
