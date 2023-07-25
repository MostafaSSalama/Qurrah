using Microsoft.AspNetCore.Mvc;

namespace Qurrah.Web.Areas.Center.Controllers
{
    [Area("Center")]
    public class AccreditationController : Controller
    {
        public ActionResult Create()
        {
            return View();
        }
    }
}
