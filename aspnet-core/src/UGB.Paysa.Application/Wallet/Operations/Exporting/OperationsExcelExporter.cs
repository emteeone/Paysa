using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using UGB.Paysa.DataExporting.Excel.NPOI;
using UGB.Paysa.Wallet.Operations.Dtos;
using UGB.Paysa.Dto;
using UGB.Paysa.Storage;

namespace UGB.Paysa.Wallet.Operations.Exporting
{
    public class OperationsExcelExporter : NpoiExcelExporterBase, IOperationsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public OperationsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetOperationForViewDto> operations)
        {
            return CreateExcelPackage(
                "Operations.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("Operations"));

                    AddHeader(
                        sheet,
                        L("CodeOperation"),
                        L("DateOperation"),
                        L("Montant"),
                        L("Discriminator"),
                        (L("Compte")) + L("NumeroCompte"),
                        (L("TypeOperation")) + L("Nom")
                        );

                    AddObjects(
                        sheet, operations,
                        _ => _.Operation.CodeOperation,
                        _ => _timeZoneConverter.Convert(_.Operation.DateOperation, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.Operation.Montant,
                        _ => _.Operation.Discriminator,
                        _ => _.CompteNumeroCompte,
                        _ => _.TypeOperationNom
                        );

                    for (var i = 1; i <= operations.Count; i++)
                    {
                        SetCellDataFormat(sheet.GetRow(i).Cells[2], "yyyy-mm-dd");
                    }
                    sheet.AutoSizeColumn(2);
                });
        }
    }
}