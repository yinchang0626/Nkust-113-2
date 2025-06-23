using CemeteryWeb.Models;
using CemeteryWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace CemeteryWeb.Controllers
{
    public class RecheckCaseController : Controller
    {
        private readonly IRecheckCaseService _service;
        public RecheckCaseController(IRecheckCaseService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var cases = await _service.GetAllCasesAsync();
            return View(cases);
        }

        public async Task<IActionResult> Details(int year, int month, string hospital)
        {
            var item = await _service.GetCaseAsync(year, month, hospital);
            if (item == null)
                return NotFound();
            return View(item);
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
                await _service.UploadCasesAsync(file);
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
