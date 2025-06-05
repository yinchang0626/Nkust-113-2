
using System.Text.Json;

public class AirQualityService
{
    private readonly IWebHostEnvironment _env;

    public AirQualityService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public List<AirQualityRecord> LoadData()
    {
        string path = Path.Combine(_env.WebRootPath, "data.json");
        if (!File.Exists(path)) return new List<AirQualityRecord>();
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<AirQualityRecord>>(json) ?? new();
    }
}
