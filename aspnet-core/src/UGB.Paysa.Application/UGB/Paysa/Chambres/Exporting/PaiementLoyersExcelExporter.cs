using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using UGB.Paysa.DataExporting.Excel.NPOI;
using UGB.Paysa.UGB.Paysa.Chambres.Dtos;
using UGB.Paysa.Dto;
using UGB.Paysa.Storage;

namespace UGB.Paysa.UGB.Paysa.Chambres.Exporting
{
    public class PaiementLoyersExcelExporter : NpoiExcelExporterBase, IPaiementLoyersExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public PaiementLoyersExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetPaiementLoyerForViewDto> paiementLoyers)
        {
            return CreateExcelPackage(
                "PaiementLoyers.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("PaiementLoyers"));

                    AddHeader(
                        sheet,
                        L("Mois"),
                        L("Annee"),
                        L("CodePaiement"),
                        (L("Chambre")) + L("Reference"),
                        (L("Operation")) + L("CodeOperation")
                        );

                    AddObjects(
                        sheet, paiementLoyers,
                        _ => _.PaiementLoyer.Mois,
                        _ => _.PaiementLoyer.Annee,
                        _ => _.PaiementLoyer.CodePaiement,
                        _ => _.ChambreReference,
                        _ => _.OperationCodeOperation
                        );

                });
        }
    }
}