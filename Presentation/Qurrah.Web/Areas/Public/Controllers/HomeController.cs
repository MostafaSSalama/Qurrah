﻿using Qurrah.Business.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Qurrah.Web.Models;
using System.Diagnostics;

namespace Qurrah.Web.Areas.Public.Controllers
{
    [Area("Public")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private LanguageService _localization;

        public HomeController(ILogger<HomeController> logger, LanguageService localization)
        {
            _logger = logger;
            _localization = localization;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        #region Localization
        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
            {
                Expires = DateTime.Now.AddYears(1)
            });
            return Redirect(Request.Headers["Referer"].ToString());
        }
        #endregion
    }
}