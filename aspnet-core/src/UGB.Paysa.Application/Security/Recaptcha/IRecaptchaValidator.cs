using System.Threading.Tasks;

namespace UGB.Paysa.Security.Recaptcha
{
    public interface IRecaptchaValidator
    {
        Task ValidateAsync(string captchaResponse);
    }
}