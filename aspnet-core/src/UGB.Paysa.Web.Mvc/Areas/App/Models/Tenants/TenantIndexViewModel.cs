using System.Collections.Generic;
using UGB.Paysa.Editions.Dto;

namespace UGB.Paysa.Web.Areas.App.Models.Tenants
{
    public class TenantIndexViewModel
    {
        public List<SubscribableEditionComboboxItemDto> EditionItems { get; set; }
    }
}