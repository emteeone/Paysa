using System.ComponentModel.DataAnnotations;

namespace UGB.Paysa.Localization.Dto
{
    public class CreateOrUpdateLanguageInput
    {
        [Required]
        public ApplicationLanguageEditDto Language { get; set; }
    }
}