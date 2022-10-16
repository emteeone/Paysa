using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using UGB.Paysa.DataExporting.Excel.NPOI;
using UGB.Paysa.Wallet.Tools.Dtos;
using UGB.Paysa.Dto;
using UGB.Paysa.Storage;

namespace UGB.Paysa.Wallet.Tools.Exporting
{
    public class CartesExcelExporter : NpoiExcelExporterBase, ICartesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public CartesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetCarteForViewDto> cartes)
        {
            return CreateExcelPackage(
                "Cartes.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("Cartes"));

                    AddHeader(
                        sheet,
                        L("UID"),
                        L("NumeroCarte"),
                        L("IsActive"),
                        L("DateDelivrance"),
                        L("DateExpiration"),
                        (L("Compte")) + L("NumeroCompte")
                        );

                    AddObjects(
                        sheet, cartes,
                        _ => _.Carte.UID,
                        _ => _.Carte.NumeroCarte,
                        _ => _.Carte.IsActive,
                        _ => _timeZoneConverter.Convert(_.Carte.DateDelivrance, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _timeZoneConverter.Convert(_.Carte.DateExpiration, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.CompteNumeroCompte
                        );

                    for (var i = 1; i <= cartes.Count; i++)
                    {
                        SetCellDataFormat(sheet.GetRow(i).Cells[4], "yyyy-mm-dd");
                    }
                    sheet.AutoSizeColumn(4); for (var i = 1; i <= cartes.Count; i++)
                    {
                        SetCellDataFormat(sheet.GetRow(i).Cells[5], "yyyy-mm-dd");
                    }
                    sheet.AutoSizeColumn(5);
                });
        }
    }
}