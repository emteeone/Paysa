using System.Collections.Generic;
using UGB.Paysa.Wallet.Etudiants.Dtos;
using UGB.Paysa.Dto;

namespace UGB.Paysa.Wallet.Etudiants.Exporting
{
    public interface IEtudiantsExcelExporter
    {
        FileDto ExportToFile(List<GetEtudiantForViewDto> etudiants);
    }
}