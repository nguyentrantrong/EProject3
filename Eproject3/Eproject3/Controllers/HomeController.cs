using Eproject3.Data;
using Eproject3.Helpers;
using Eproject3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static Eproject3.Data.IDAL;

namespace Eproject3.Controllers
{
    public static class SchemesNameConst
    {
        public const string TokenAuthenticationDefaultScheme = "TokenAuthenticationScheme";
    }
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDAL _idal;
        private readonly eProject3Context db;

        public HomeController(ILogger<HomeController> logger, IDAL idal, eProject3Context db)
        {
            _logger = logger;
            _idal = idal;
            this.db = db;
        }
        [Authorize(Roles = "admin, user, staff")]
        public IActionResult Index()
        {
            HttpContext.Session.GetString("adminId");
            //ViewData["Resources"] = JSONListHelper.GetResourceListJSONString(db.Labs.ToList());
            //ViewData["Events"] = JSONListHelper.GetEventListJSONString(db.Events.ToList());
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
        public ViewResult PageNotFound()
        {
            Response.StatusCode = 404;
            return View();
        }
    }
}