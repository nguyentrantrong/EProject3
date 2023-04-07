using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Eproject3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        private readonly IConfiguration _config;
        public AccountController(eProject3Context _db, IConfiguration config)
        {
            db = _db;
            _config = config;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string uname, string password)
        {
            var user = await db.Admins.SingleOrDefaultAsync(u => u.AdminName.Equals(uname));
            if (user != null && user.Password.Equals(password) && user.IsActive == true)
            {
                var token = GenerateJwtToken(user);

                // Save token in cookie
                Response.Cookies.Append("jwtToken", token, new CookieOptions { HttpOnly = true, Secure = true, SameSite = SameSiteMode.None });
                return RedirectToAction("Index", "Home");
            }
            else if(user.IsActive == false)
            {
                ViewBag.msgbl = "YOUR ACCOUNT HAS BEEN BLOCK! PLEASE INPUT ANOTHER ACCOUNT!";
            }
            else
            {
                ViewBag.msg = "USERNAME OR PASSWORD IS INCORRECT! PLEASE TRY AGAIN!";
            }
            return View();
        }

        public IActionResult Logout()
        {
            // Delete token in cookie
            HttpContext.Response.Cookies.Delete("jwtToken");
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Index()
        {
                var res = db.Admins.ToList();
                return View(res);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
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
                    return RedirectToAction("Index", "Account");
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

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(string id)
        {
            var users = db.Admins.Where(u => u.Id.Equals(id)).FirstOrDefault();
            return View(users);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(Admin admin)
        {
            var model = db.Admins.Where(d => d.Id.Equals(admin.Id)).FirstOrDefault();
			if (model != null)
            {
                model.Id = admin.Id;
                model.AdminName = admin.AdminName;
                model.Password = admin.Password;
                model.Role = admin.Role;
                model.IsActive = admin.IsActive;
                db.Admins.Update(model);
                db.SaveChanges();
                return RedirectToAction("Index", "Account");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Cannot Update Account!");
            }
            return View(model);
        }

        private string GenerateJwtToken(Admin user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _config["Jwt:Audience"],
                Issuer = _config["Jwt:Issuer"],
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.AdminName),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}

