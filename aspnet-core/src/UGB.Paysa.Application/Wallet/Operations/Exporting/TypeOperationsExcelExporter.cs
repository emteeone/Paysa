using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using UGB.Paysa.DataExporting.Excel.NPOI;
using UGB.Paysa.Wallet.Operations.Dtos;
using UGB.Paysa.Dto;
using UGB.Paysa.Storage;

namespace UGB.Paysa.Wallet.Operations.Exporting
{
    public class TypeOperationsExcelExporter : NpoiExcelExporterBase, ITypeOperationsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public TypeOperationsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetTypeOperationForViewDto> typeOperations)
        {
            return CreateExcelPackage(
                "TypeOperations.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("TypeOperations"));

                    AddHeader(
                        sheet,
                        L("Reference"),
                        L("Nom"),
                        L("Prix")
                        );

                    AddObjects(
                        sheet, typeOperations,
                        _ => _.TypeOperation.Reference,
                        _ => _.TypeOperation.Nom,
                        _ => _.TypeOperation.Prix
                        );

                });
        }
    }
}