using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using UGB.Paysa.DataExporting.Excel.NPOI;
using UGB.Paysa.Wallet.Chambres.Dtos;
using UGB.Paysa.Dto;
using UGB.Paysa.Storage;

namespace UGB.Paysa.Wallet.Chambres.Exporting
{
    public class ChambresExcelExporter : NpoiExcelExporterBase, IChambresExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ChambresExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetChambreForViewDto> chambres)
        {
            return CreateExcelPackage(
                "Chambres.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("Chambres"));

                    AddHeader(
                        sheet,
                        L("Reference"),
                        L("NumeroChambre"),
                        L("Bloc"),
                        L("Village"),
                        L("Campus"),
                        L("MontantLocation")
                        );

                    AddObjects(
                        sheet, chambres,
                        _ => _.Chambre.Reference,
                        _ => _.Chambre.NumeroChambre,
                        _ => _.Chambre.Bloc,
                        _ => _.Chambre.Village,
                        _ => _.Chambre.Campus,
                        _ => _.Chambre.MontantLocation
                        );

                });
        }
    }
}