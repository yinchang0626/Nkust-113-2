using CemeteryWeb.Models;
using CemeteryWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CemeteryWeb.Controllers
{
    public class CareServiceController : Controller
    {
        private readonly ICareServiceService _careServiceService;

        public CareServiceController(ICareServiceService careServiceService)
        {
            _careServiceService = careServiceService;
        }

        public async Task<IActionResult> Index()
        {
            var careServices = await _careServiceService.GetAllCareServicesAsync();
            return View(careServices);
        }

        public async Task<IActionResult> Details(string name)
        {
            var careService = await _careServiceService.GetCareServiceByNameAsync(name);
            if (careService is null)
                return NotFound();
            return View("Details", careService);
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    TempData["Error"] = "請選擇有效的 CSV 檔案";
                    return RedirectToAction(nameof(Upload));
                }

                await _careServiceService.UploadCareServiceDataAsync(file);
                TempData["Success"] = $"已成功上傳 {file.FileName}";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"上傳失敗: {ex.Message}";
                return View();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
