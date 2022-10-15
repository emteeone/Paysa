using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using UGB.Paysa.Dto;

namespace UGB.Paysa.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}
