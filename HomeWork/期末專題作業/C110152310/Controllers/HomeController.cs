using Microsoft.AspNetCore.Mvc;

namespace C110152310.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("login") != "true")
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("login");
            return RedirectToAction("Index", "Login");
        }
    }
}
