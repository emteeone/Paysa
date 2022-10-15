using System.ComponentModel.DataAnnotations;

namespace UGB.Paysa.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
