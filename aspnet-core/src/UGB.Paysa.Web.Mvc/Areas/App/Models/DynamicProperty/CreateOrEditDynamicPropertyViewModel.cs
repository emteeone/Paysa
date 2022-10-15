using System.Collections.Generic;
using UGB.Paysa.DynamicEntityProperties.Dto;

namespace UGB.Paysa.Web.Areas.App.Models.DynamicProperty
{
    public class CreateOrEditDynamicPropertyViewModel
    {
        public DynamicPropertyDto DynamicPropertyDto { get; set; }

        public List<string> AllowedInputTypes { get; set; }
    }
}
