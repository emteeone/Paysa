using System.Collections.Generic;
using UGB.Paysa.Authorization.Users.Importing.Dto;
using UGB.Paysa.Dto;

namespace UGB.Paysa.Authorization.Users.Importing
{
    public interface IInvalidUserExporter
    {
        FileDto ExportToFile(List<ImportUserDto> userListDtos);
    }
}
