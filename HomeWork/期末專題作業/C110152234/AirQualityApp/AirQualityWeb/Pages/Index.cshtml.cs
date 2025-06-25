using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using System;

public class IndexModel : PageModel
{
    public List<AirQualityRecord>? Records { get; set; }

    public void OnGet()
    {
        string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data.json");
        if (System.IO.File.Exists(jsonPath))
        {
            string json = System.IO.File.ReadAllText(jsonPath);
            Records = JsonSerializer.Deserialize<List<AirQualityRecord>>(json);
        }
        else
        {
            Records = new List<AirQualityRecord>();
        }
    }
}
