using System.Collections.Generic;
using UGB.Paysa.UGB.Paysa.Chambres.Dtos;
using UGB.Paysa.Dto;

namespace UGB.Paysa.UGB.Paysa.Chambres.Exporting
{
    public interface IPaiementLoyersExcelExporter
    {
        FileDto ExportToFile(List<GetPaiementLoyerForViewDto> paiementLoyers);
    }
}