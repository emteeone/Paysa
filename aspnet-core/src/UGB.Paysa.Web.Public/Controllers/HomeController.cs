using Microsoft.AspNetCore.Mvc;
using UGB.Paysa.Web.Controllers;

namespace UGB.Paysa.Web.Public.Controllers
{
    public class HomeController : PaysaControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}