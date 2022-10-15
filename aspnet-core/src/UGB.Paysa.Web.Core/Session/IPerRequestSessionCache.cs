using System.Threading.Tasks;
using UGB.Paysa.Sessions.Dto;

namespace UGB.Paysa.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
