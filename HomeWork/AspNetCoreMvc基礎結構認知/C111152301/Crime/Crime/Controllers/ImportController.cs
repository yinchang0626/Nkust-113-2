using Crime.Data;
using Microsoft.AspNetCore.Mvc;

namespace Crime.Controllers
{
    [Route("Import")]
    public class ImportController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CsvService _csvService;
        private readonly IWebHostEnvironment _env;

        public ImportController(ApplicationDbContext context, CsvService csvService, IWebHostEnvironment env)
        {
            _context = context;
            _csvService = csvService;
            _env = env;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                int count = await _csvService.ImportCsvAsync(stream);
                ViewBag.Message = $"成功匯入 {count} 筆資料";
            }
            return View();
        }
    }
}
