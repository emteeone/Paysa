using System.Collections.Generic;
using UGB.Paysa.Wallet.Comptes.Dtos;
using UGB.Paysa.Dto;

namespace UGB.Paysa.Wallet.Comptes.Exporting
{
    public interface IComptesExcelExporter
    {
        FileDto ExportToFile(List<GetCompteForViewDto> comptes);
    }
}