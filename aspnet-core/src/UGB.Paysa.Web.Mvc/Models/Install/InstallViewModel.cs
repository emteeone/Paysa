using System.Collections.Generic;
using Abp.Localization;
using UGB.Paysa.Install.Dto;

namespace UGB.Paysa.Web.Models.Install
{
    public class InstallViewModel
    {
        public List<ApplicationLanguage> Languages { get; set; }

        public AppSettingsJsonDto AppSettingsJson { get; set; }
    }
}
