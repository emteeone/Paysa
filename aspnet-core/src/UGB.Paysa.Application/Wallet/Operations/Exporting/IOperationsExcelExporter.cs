using System.Collections.Generic;
using UGB.Paysa.Wallet.Operations.Dtos;
using UGB.Paysa.Dto;

namespace UGB.Paysa.Wallet.Operations.Exporting
{
    public interface IOperationsExcelExporter
    {
        FileDto ExportToFile(List<GetOperationForViewDto> operations);
    }
}