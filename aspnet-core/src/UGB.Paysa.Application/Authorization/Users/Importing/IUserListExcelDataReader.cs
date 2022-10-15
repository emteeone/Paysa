using System.Collections.Generic;
using UGB.Paysa.Authorization.Users.Importing.Dto;
using Abp.Dependency;

namespace UGB.Paysa.Authorization.Users.Importing
{
    public interface IUserListExcelDataReader: ITransientDependency
    {
        List<ImportUserDto> GetUsersFromExcel(byte[] fileBytes);
    }
}
