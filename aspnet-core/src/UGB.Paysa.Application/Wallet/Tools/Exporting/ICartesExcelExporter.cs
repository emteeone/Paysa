using System.Collections.Generic;
using UGB.Paysa.Wallet.Tools.Dtos;
using UGB.Paysa.Dto;

namespace UGB.Paysa.Wallet.Tools.Exporting
{
    public interface ICartesExcelExporter
    {
        FileDto ExportToFile(List<GetCarteForViewDto> cartes);
    }
}