
using Microsoft.AspNetCore.Mvc;

public class AirQualityController : Controller
{
    private readonly AirQualityService _service;

    public AirQualityController(AirQualityService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        var data = _service.LoadData();
        return View(data);
    }
}
