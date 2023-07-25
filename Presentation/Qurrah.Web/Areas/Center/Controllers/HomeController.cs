using Microsoft.AspNetCore.Mvc;

namespace Qurrah.Web.Areas.Center.Controllers
{
    [Area("Center")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
