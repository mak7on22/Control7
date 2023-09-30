using KR.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using Microsoft.EntityFrameworkCore;

namespace KR.Controllers
{
    public class AccountController : Controller
    {
        private BookStoragePortalContext _db;

        public AccountController(BookStoragePortalContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUser model)
        {
            if (ModelState.IsValid)
            {
                User user = await _db.Users.Include(user => user.Role).FirstOrDefaultAsync(u => u.UserEmail == model.LogUserEmail);
                if (user != null)
                {
                    await Authenticate(user); // аутентификация
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректный логин");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUser model)
        {
            if (ModelState.IsValid)
            {
                User user = await _db.Users.FirstOrDefaultAsync(u => u.UserEmail == model.RegUserEmail);
                if (user == null)
                {
                    Role role = _db.Roles.FirstOrDefault(r => r.RoleName == "User");
                    var newUser = new User
                    {
                        UserEmail = model.RegUserEmail,
                        UserName = model.RegUserName,
                        UserFirstName = model.RegUserFirstName,
                        UserPhone = model.RegUserPhone,
                        RoleId = role.RoleId
                      
                    };

                    _db.Users.Add(newUser);
                    await _db.SaveChangesAsync();
                    await Authenticate(newUser);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректный логин");
            }
            return View(model);
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType,user.UserEmail),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,user.Role?.RoleName)
            };
            ClaimsIdentity id = new ClaimsIdentity(
                claims,
                "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(id),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(300)
                }
     );
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
