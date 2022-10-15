using System.Collections.Generic;
using UGB.Paysa.Wallet.Chambres.Dtos;
using UGB.Paysa.Dto;

namespace UGB.Paysa.Wallet.Chambres.Exporting
{
    public interface IChambresExcelExporter
    {
        FileDto ExportToFile(List<GetChambreForViewDto> chambres);
    }
}