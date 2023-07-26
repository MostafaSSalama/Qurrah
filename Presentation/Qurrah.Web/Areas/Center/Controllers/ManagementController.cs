using Microsoft.AspNetCore.Mvc;

namespace Qurrah.Web.Areas.Center.Controllers
{
    [Area("Center")]
    public class ManagementController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
