using System.Collections.Generic;
using UGB.Paysa.Auditing.Dto;
using UGB.Paysa.Dto;

namespace UGB.Paysa.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);

        FileDto ExportToFile(List<EntityChangeListDto> entityChangeListDtos);
    }
}
