using System.ComponentModel.DataAnnotations;

namespace UGB.Paysa.Authorization.Accounts.Dto
{
    public class SendEmailActivationLinkInput
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}