using Microsoft.AspNetCore.Mvc;
using UGB.Paysa.Web.Controllers;

namespace UGB.Paysa.Web.Public.Controllers
{
    public class AboutController : PaysaControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}