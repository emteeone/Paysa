using System.Collections.Generic;
using UGB.Paysa.Wallet.Tools.Dtos;
using UGB.Paysa.Dto;

namespace UGB.Paysa.Wallet.Tools.Exporting
{
    public interface ITerminauxExcelExporter
    {
        FileDto ExportToFile(List<GetTerminalForViewDto> terminaux);
    }
}