using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApp.Data;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class RescueController : Controller
    {
        private readonly IRescueService _rescueService;
        private readonly AppDbContext _appDbContext;

        public RescueController(IRescueService rescueService, AppDbContext appDbContext)
        {
            _rescueService = rescueService;
            _appDbContext = appDbContext;
        }

        // 首頁顯示資料（用 Service 載入 OpenData）
        public IActionResult Index()
        {
            var data = _rescueService.LoadData();
            return View(data);
        }

        // 匯入資料到資料庫
        public IActionResult ImportData()
        {
            if (!_appDbContext.RescueDatas.Any())
            {
                var data = _rescueService.LoadData();
                _appDbContext.RescueDatas.AddRange(data);
                _appDbContext.SaveChanges();
                ViewBag.Message = "Data successfully imported.";
            }
            else
            {
                ViewBag.Message = "Data already exists. Skipped import.";
            }

            return View();
        }
    }
}
