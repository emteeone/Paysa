using Microsoft.AspNetCore.Antiforgery;

namespace UGB.Paysa.Web.Controllers
{
    public class AntiForgeryController : PaysaControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
