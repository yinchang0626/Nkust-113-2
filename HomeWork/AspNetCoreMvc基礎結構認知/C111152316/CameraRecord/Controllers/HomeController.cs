using CameraRecord;
using CameraRecord.Models; // 你自己的命名空間
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace CameraRecord.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public HomeController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index()
        {
            string csvPath = Path.Combine(_env.WebRootPath, "data", "CameraRecord.csv");

            List<Record> records = new();
            if (System.IO.File.Exists(csvPath))
            {
                using var reader = new StreamReader(csvPath, Encoding.UTF8);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                records = csv.GetRecords<Record>().ToList();
            }

            return View(records);
        }
        public IActionResult Details(string id)
        {
            string csvPath = Path.Combine(_env.WebRootPath, "data", "CameraRecord.csv");

            if (!System.IO.File.Exists(csvPath))
                return NotFound();

            using var reader = new StreamReader(csvPath, Encoding.UTF8);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<Record>().ToList();

            var record = records.FirstOrDefault(r => r.編號 == id);

            if (record == null)
                return NotFound();

            return View(record);
        }
        [HttpGet]
        public IActionResult Upload() => View();

        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("請選擇檔案");

            List<Record> records;
            using (var stream = file.OpenReadStream())
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                records = csv.GetRecords<Record>().ToList();
            }

            return View("UploadResult", records);
        }





        public IActionResult UploadResult()
        {
            if (TempData["UploadResult"] is string json)
            {
                var records = JsonSerializer.Deserialize<List<Record>>(json);
                return View(records);
            }

            return RedirectToAction("Index");
        }

        public IActionResult GenerateReport()
        {
            string csvPath = Path.Combine(_env.WebRootPath, "data", "CameraRecord.csv");

            if (!System.IO.File.Exists(csvPath))
                return NotFound();

            using var reader = new StreamReader(csvPath, Encoding.UTF8);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<Record>().ToList();

            var report = records
                .GroupBy(r => r.行政區)
                .Select(g => new DistrictReportViewModel
                {
                    行政區 = g.Key,
                    總數量 = g.Count()
                })
                .ToList();

            return View(report);
        }


     }

}
