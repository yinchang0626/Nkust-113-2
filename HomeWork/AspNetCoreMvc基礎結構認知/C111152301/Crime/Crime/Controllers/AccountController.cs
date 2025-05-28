using Crime.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Xml.Linq;

public class UserAccount
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } = "User";
}

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var user = await _context.UserAccounts.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("role", user.Role)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            TempData["LoginMessage"] = "登入成功";
            return RedirectToAction("Index", "CrimeStats");
        }

        TempData["LoginMessage"] = "登入失敗，請重新輸入";
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        TempData["LogoutMessage"] = "您已登出";
        return RedirectToAction("Index", "CrimeStats");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(string username, string password)
    {
        if (_context.UserAccounts.Any(u => u.Username == username))
        {
            TempData["LoginMessage"] = "帳號已存在";
            return RedirectToAction("Register");
        }

        _context.UserAccounts.Add(new UserAccount { Username = username, Password = password, Role = "User" });
        await _context.SaveChangesAsync();
        TempData["LoginMessage"] = "註冊成功，請登入";
        return RedirectToAction("Login");
    }
    [Authorize(Roles = "Admin")]
    public IActionResult ManageUsers()
    {
        return View(_context.UserAccounts.ToList());
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Promote(int id)
    {
        var user = _context.UserAccounts.Find(id);
        if (user != null) { user.Role = "Admin"; _context.SaveChanges(); }
        return RedirectToAction("ManageUsers");
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Demote(int id)
    {
        var user = _context.UserAccounts.Find(id);
        if (user != null) { user.Role = "User"; _context.SaveChanges(); }
        return RedirectToAction("ManageUsers");
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        var user = _context.UserAccounts.Find(id);
        if (user != null && user.Username != User.Identity.Name)
        {
            _context.UserAccounts.Remove(user);
            _context.SaveChanges();
        }
        return RedirectToAction("ManageUsers");
    }

}