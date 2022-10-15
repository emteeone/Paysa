using System.Collections.Generic;
using UGB.Paysa.Authorization.Users.Dto;
using UGB.Paysa.Dto;

namespace UGB.Paysa.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}