using System.Collections.Generic;
using Abp;
using UGB.Paysa.Chat.Dto;
using UGB.Paysa.Dto;

namespace UGB.Paysa.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(UserIdentifier user, List<ChatMessageExportDto> messages);
    }
}
