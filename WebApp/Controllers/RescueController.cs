using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class RescueController : Controller
    {
        private readonly IRescueService _rescueService;

        public RescueController(IRescueService rescueService)
        {
            _rescueService = rescueService;
        }

        // Index action to display rescue data
        public IActionResult Index()
        {
            var data = _rescueService.LoadData();
            return View(data);
        }

    }
}
