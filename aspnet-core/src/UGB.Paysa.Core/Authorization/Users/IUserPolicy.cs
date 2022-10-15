using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace UGB.Paysa.Authorization.Users
{
    public interface IUserPolicy : IPolicy
    {
        Task CheckMaxUserCountAsync(int tenantId);
    }
}
