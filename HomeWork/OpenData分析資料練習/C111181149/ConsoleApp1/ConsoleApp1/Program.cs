using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AirQualityJsonStats
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string filePath = "aqx_p_432.json";
            var records = ReadJson(filePath);

            Console.WriteLine($"資料總筆數：{records.Count}");

            Console.WriteLine("\n前5筆資料（測站, 縣市, PM2.5, 發布時間）：");
            foreach (var record in records.Take(5))
            {
                Console.WriteLine($"{record.SiteName} - {record.County} - PM2.5: {record.PM25} - 時間: {record.PublishTime}");
            }

            Console.WriteLine("\n=== 統計資訊 ===");

            var pm25Values = records
                .Where(r => double.TryParse(r.PM25, out _))
                .Select(r => double.Parse(r.PM25))
                .ToList();

            if (pm25Values.Count > 0)
            {
                Console.WriteLine($"PM2.5 平均值：{pm25Values.Average():0.00}");
                Console.WriteLine($"PM2.5 最大值：{pm25Values.Max()}");
                Console.WriteLine($"PM2.5 最小值：{pm25Values.Min()}");
            }
            else
            {
                Console.WriteLine("找不到有效的 PM2.5 數值。");
            }

            var highest = records
                .Where(r => double.TryParse(r.PM25, out _))
                .OrderByDescending(r => double.Parse(r.PM25))
                .FirstOrDefault();

            if (highest != null)
            {
                Console.WriteLine($"\nPM2.5 最高測站：{highest.SiteName}（{highest.County}） - PM2.5: {highest.PM25}");
            }
        }

        public static List<AirQualityRecord> ReadJson(string path)
        {
            var jsonString = File.ReadAllText(path);
            var root = JsonSerializer.Deserialize<AirQualityRoot>(jsonString);
            return root?.Records ?? new List<AirQualityRecord>();
        }
    }

    public class AirQualityRoot
    {
        [JsonPropertyName("records")]
        public List<AirQualityRecord> Records { get; set; }
    }

    public class AirQualityRecord
    {
        [JsonPropertyName("sitename")]
        public string SiteName { get; set; }

        [JsonPropertyName("county")]
        public string County { get; set; }

        [JsonPropertyName("pm2.5")]
        public string PM25 { get; set; }

        [JsonPropertyName("publishtime")]
        public string PublishTime { get; set; }
    }
}
