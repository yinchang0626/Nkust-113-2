using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace C110152310.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            if (username == "user" && password == "123456")
            {
                HttpContext.Session.SetString("login", "true");
                return RedirectToAction("Index", "Courses");
            }
            ViewBag.Error = "帳號或密碼錯誤";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("login");
            return RedirectToAction("Index", "Login");
        }
    }
}