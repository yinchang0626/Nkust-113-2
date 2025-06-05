using Microsoft.AspNetCore.Mvc;
using WeatherDataMVC.Services;
using WeatherDataMVC.Models;

namespace WeatherDataMVC.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public IActionResult Index()
        {
            var stations = _weatherService.GetAllStations();
            return View(stations);
        }

        public IActionResult Details(string id)
        {
            var station = _weatherService.GetStationById(id);
            if (station == null) return NotFound();
            return View(station);
        }

        [HttpGet]
        public IActionResult Upload() => View();

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using var reader = new StreamReader(file.OpenReadStream());
                var json = await reader.ReadToEndAsync();
                _weatherService.LoadFromJson(json);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult GenerateReport()
        {
            var stations = _weatherService.GetAllStations();
            var report = new
            {
                Count = stations.Count,
                AvgSolarRadiation = stations.Average(s => double.TryParse(s.WeatherElement?.SolarRadiation, out var val) ? val : 0)
            };
            return View(report);
        }
    }
}