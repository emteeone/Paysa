using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using UGB.Paysa.DataExporting.Excel.NPOI;
using UGB.Paysa.Wallet.Etudiants.Dtos;
using UGB.Paysa.Dto;
using UGB.Paysa.Storage;

namespace UGB.Paysa.Wallet.Etudiants.Exporting
{
    public class EtudiantsExcelExporter : NpoiExcelExporterBase, IEtudiantsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public EtudiantsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetEtudiantForViewDto> etudiants)
        {
            return CreateExcelPackage(
                "Etudiants.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("Etudiants"));

                    AddHeader(
                        sheet,
                        L("INE"),
                        L("CodeEtudiant"),
                        L("Prenom"),
                        L("Nom"),
                        L("Sexe"),
                        L("DateDeNaissance"),
                        L("LieuDeNaissance"),
                        L("SituationMatrimoniale"),
                        L("CIN"),
                        L("Adresse"),
                        L("Ville"),
                        L("Region"),
                        L("Pays"),
                        L("Email"),
                        L("Telephone"),
                        (L("Chambre")) + L("Reference"),
                        (L("User")) + L("Name")
                        );

                    AddObjects(
                        sheet, etudiants,
                        _ => _.Etudiant.INE,
                        _ => _.Etudiant.CodeEtudiant,
                        _ => _.Etudiant.Prenom,
                        _ => _.Etudiant.Nom,
                        _ => _.Etudiant.Sexe,
                        _ => _timeZoneConverter.Convert(_.Etudiant.DateDeNaissance, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.Etudiant.LieuDeNaissance,
                        _ => _.Etudiant.SituationMatrimoniale,
                        _ => _.Etudiant.CIN,
                        _ => _.Etudiant.Adresse,
                        _ => _.Etudiant.Ville,
                        _ => _.Etudiant.Region,
                        _ => _.Etudiant.Pays,
                        _ => _.Etudiant.Email,
                        _ => _.Etudiant.Telephone,
                        _ => _.ChambreReference,
                        _ => _.UserName
                        );

                    for (var i = 1; i <= etudiants.Count; i++)
                    {
                        SetCellDataFormat(sheet.GetRow(i).Cells[6], "yyyy-mm-dd");
                    }
                    sheet.AutoSizeColumn(6);
                });
        }
    }
}