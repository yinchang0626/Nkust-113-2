using Microsoft.AspNetCore.Mvc;
using C110196130.Services;
using C110196130.Models;
using System.IO;
using System.Linq;

public class DivorceRecordsController : Controller
{
    private readonly CsvService _csvService;
    private readonly string _csvFilePath;

    public DivorceRecordsController(CsvService csvService)
    {
        _csvService = csvService;
        // 使用相對路徑組合完整檔案路徑
        _csvFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "臺北市人口離婚情形時間數列統計資料.csv");
    }

    // 顯示資料列表
    public IActionResult Index()
    {
        var records = _csvService.ReadCsvFile(_csvFilePath);
        return View(records);
    }

    // 顯示詳細資訊
    public IActionResult Details(string id)
    {
        var record = _csvService.ReadCsvFile(_csvFilePath)
                                .FirstOrDefault(r => r.統計期 == id);
        if (record == null)
            return NotFound();
        return View(record);
    }

    // 上傳 CSV 檔案並解析
    [HttpPost]
    public IActionResult Upload(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            using var stream = file.OpenReadStream();
            var records = _csvService.ReadCsvFile(stream);
            // 處理解析後的資料，例如儲存或顯示
        }
        return RedirectToAction("Index");
    }

    // 生成並顯示報表
    public IActionResult GenerateReport()
    {
        var records = _csvService.ReadCsvFile(_csvFilePath);
        var report = new
        {
            MaxDivorceCount = records.Max(r => r.離婚對數總計),
            MinDivorceCount = records.Min(r => r.離婚對數總計),
            AvgAgeMale = records.Average(r => r.平均離婚年齡男)
        };
        return View(report);
    }
}
