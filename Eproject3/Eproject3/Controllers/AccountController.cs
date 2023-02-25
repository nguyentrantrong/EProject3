using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Eproject3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Eproject3.Controllers
{
    [Authorize(AuthenticationSchemes = SchemesNamesConst.TokenAuthenticationDefaultScheme)]
    public static class SchemesNamesConst
    {
        public const string TokenAuthenticationDefaultScheme = "TokenAuthenticationScheme";
    }
    public class AccountController : Controller
    {
        private eProject3Context db;
        public AccountController(eProject3Context _db)
        {
            db = _db;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string uname, string password)
        {
            try
            {
                var user = await db.Admins.SingleOrDefaultAsync(u => u.AdminName.Equals(uname));
                if (user != null && user.Password.Equals(password))
                {
                    HttpContext.Session.SetString("adminName", user.AdminName);
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ViewBag.msg = "USERNAME OR PASSWORD IS INCORRECT! PLEASE TRY AGAIN!";
                }
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("adminName");
                
            return RedirectToAction("Login", "Account");
        }

        [Authorize(Policy = "AdminOnly, StaffOnly, UserOnly")]
        public IActionResult Layout(Admin user)
        {
            if (HttpContext.Session.GetString("adminName") == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var res = db.Admins.ToList();
                return View(res);
            }
        }

        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Create(Admin newAdmin)
        {
            var users = db.Admins.Where(u => u.Id.Equals(newAdmin.Id)).FirstOrDefault();
            //var users = db.Users.SingleOrDefault(u => u.id.Equals(newUser.id));
            try
            {
                if (ModelState.IsValid)
                {
                    db.Admins.Add(newAdmin);
                    db.SaveChanges();
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Fail!");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }
        

    }
}

