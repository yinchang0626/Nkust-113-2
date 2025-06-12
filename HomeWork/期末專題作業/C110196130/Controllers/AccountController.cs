using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CourseSelectionSystem.Models;
using CourseSelectionSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace CourseSelectionSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        public AccountController(AppDbContext context)
        {
            _context = context;
            EnsureDefaultAdmins(_context);
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public static void EnsureDefaultAdmins(AppDbContext context)
        {
            // 新增 kerong
            if (!context.Users.Any(u => u.Username == "kerong"))
            {
                var admin = new User
                {
                    Username = "kerong",
                    PasswordHash = HashPassword("abc123"),
                    Role = "Admin"
                };
                context.Users.Add(admin);
            }
            // 新增 sinewave
            if (!context.Users.Any(u => u.Username == "sinewave"))
            {
                var admin2 = new User
                {
                    Username = "sinewave",
                    PasswordHash = HashPassword("abc123"),
                    Role = "Admin"
                };
                context.Users.Add(admin2);
            }
            context.SaveChanges();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null || user.PasswordHash != HashPassword(password))
            {
                ViewBag.Error = "帳號或密碼錯誤";
                return View();
            }
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);
            return RedirectToAction("Index", "Courses");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string username, string password, string confirmPassword, string? email = null)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "帳號與密碼不得為空";
                return View();
            }
            if (password != confirmPassword)
            {
                ViewBag.Error = "密碼與確認密碼不一致";
                return View();
            }
            if (_context.Users.Any(u => u.Username == username))
            {
                ViewBag.Error = "帳號已存在";
                return View();
            }
            var user = new User { Username = username, PasswordHash = HashPassword(password), Email = email, Role = "User" };
            _context.Users.Add(user);
            _context.SaveChanges();
            ViewBag.Success = "註冊成功，請登入。";
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null || user.PasswordHash != HashPassword(oldPassword))
            {
                ViewBag.Error = "舊密碼錯誤";
                return View();
            }
            if (newPassword != confirmPassword)
            {
                ViewBag.Error = "新密碼與確認密碼不一致";
                return View();
            }
            user.PasswordHash = HashPassword(newPassword);
            _context.SaveChanges();
            ViewBag.Success = "密碼變更成功";
            return View();
        }
    }
}
