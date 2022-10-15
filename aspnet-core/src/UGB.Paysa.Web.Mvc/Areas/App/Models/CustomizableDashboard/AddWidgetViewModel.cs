using System.Collections.Generic;
using UGB.Paysa.DashboardCustomization.Dto;

namespace UGB.Paysa.Web.Areas.App.Models.CustomizableDashboard
{
    public class AddWidgetViewModel
    {
        public List<WidgetOutput> Widgets { get; set; }

        public string DashboardName { get; set; }

        public string PageId { get; set; }
    }
}
