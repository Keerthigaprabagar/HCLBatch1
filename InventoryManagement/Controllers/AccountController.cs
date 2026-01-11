using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Data;
using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace InventoryManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AccountController(ApplicationDbContext db) => _db = db;

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password, string fullname)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Username and password required";
                return View();
            }

            if (await _db.Users.AnyAsync(u => u.Username == username))
            {
                ViewBag.Error = "Username taken";
                return View();
            }

            var user = new User { Username = username, PasswordHash = SimpleHasher.Hash(password), FullName = fullname };
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Login(string? returnUrl) { ViewBag.ReturnUrl = returnUrl; return View(); }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string? returnUrl)
        {
            var hash = SimpleHasher.Hash(password);
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == hash);
            if (user == null) { ViewBag.Error = "Invalid credentials"; return View(); }

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Username), new Claim("FullName", user.FullName ?? ""), new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        public IActionResult AccessDenied() => View();
    }
}
