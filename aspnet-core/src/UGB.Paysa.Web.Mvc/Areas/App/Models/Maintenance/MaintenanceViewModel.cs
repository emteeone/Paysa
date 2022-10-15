using System.Collections.Generic;
using UGB.Paysa.Caching.Dto;

namespace UGB.Paysa.Web.Areas.App.Models.Maintenance
{
    public class MaintenanceViewModel
    {
        public IReadOnlyList<CacheDto> Caches { get; set; }
    }
}