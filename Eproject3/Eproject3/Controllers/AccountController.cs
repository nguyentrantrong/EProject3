using Microsoft.AspNetCore.Mvc;

namespace Eproject3.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
