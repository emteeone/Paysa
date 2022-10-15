using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using UGB.Paysa.DataExporting.Excel.NPOI;
using UGB.Paysa.Wallet.Comptes.Dtos;
using UGB.Paysa.Dto;
using UGB.Paysa.Storage;

namespace UGB.Paysa.Wallet.Comptes.Exporting
{
    public class ComptesExcelExporter : NpoiExcelExporterBase, IComptesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ComptesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetCompteForViewDto> comptes)
        {
            return CreateExcelPackage(
                "Comptes.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("Comptes"));

                    AddHeader(
                        sheet,
                        L("NumeroCompte"),
                        L("Solde"),
                        L("IsActive"),
                        L("DateCreation"),
                        (L("Etudiant")) + L("CodeEtudiant")
                        );

                    AddObjects(
                        sheet, comptes,
                        _ => _.Compte.NumeroCompte,
                        _ => _.Compte.Solde,
                        _ => _.Compte.IsActive,
                        _ => _timeZoneConverter.Convert(_.Compte.DateCreation, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.EtudiantCodeEtudiant
                        );

                    for (var i = 1; i <= comptes.Count; i++)
                    {
                        SetCellDataFormat(sheet.GetRow(i).Cells[4], "yyyy-mm-dd");
                    }
                    sheet.AutoSizeColumn(4);
                });
        }
    }
}