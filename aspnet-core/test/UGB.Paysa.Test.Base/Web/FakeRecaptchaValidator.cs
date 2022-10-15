using System.Threading.Tasks;
using UGB.Paysa.Security.Recaptcha;

namespace UGB.Paysa.Test.Base.Web
{
    public class FakeRecaptchaValidator : IRecaptchaValidator
    {
        public Task ValidateAsync(string captchaResponse)
        {
            return Task.CompletedTask;
        }
    }
}
