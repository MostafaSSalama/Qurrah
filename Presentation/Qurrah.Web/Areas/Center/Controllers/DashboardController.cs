using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Qurrah.Web.Areas.Center.Controllers
{
    [Area("Center")]
    [Authorize(Roles = "Center")]
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
