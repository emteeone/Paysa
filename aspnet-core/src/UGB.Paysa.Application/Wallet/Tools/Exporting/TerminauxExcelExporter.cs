using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using UGB.Paysa.DataExporting.Excel.NPOI;
using UGB.Paysa.Wallet.Tools.Dtos;
using UGB.Paysa.Dto;
using UGB.Paysa.Storage;

namespace UGB.Paysa.Wallet.Tools.Exporting
{
    public class TerminauxExcelExporter : NpoiExcelExporterBase, ITerminauxExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public TerminauxExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetTerminalForViewDto> terminaux)
        {
            return CreateExcelPackage(
                "Terminaux.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("Terminaux"));

                    AddHeader(
                        sheet,
                        L("Uid_Device"),
                        L("Matricule"),
                        L("Position")
                        );

                    AddObjects(
                        sheet, terminaux,
                        _ => _.Terminal.Uid_Device,
                        _ => _.Terminal.Matricule,
                        _ => _.Terminal.Position
                        );

                });
        }
    }
}