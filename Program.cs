// See https://aka.ms/new-console-template for more information
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

class Program
{
    static void Main()
    {
        string filePath = "5678.json"; // 確保檔案放在正確路徑
        if (!File.Exists(filePath))
        {
            Console.WriteLine("JSON 檔案不存在！");
            return;
        }

        // 讀取並解析 JSON
        string jsonData = File.ReadAllText(filePath);
        List<GasStation>? stations = JsonConvert.DeserializeObject<List<GasStation>>(jsonData);

        if (stations == null)
        {
            Console.WriteLine("解析 JSON 失敗！");
            return;
        }

        // 顯示總筆數
        Console.WriteLine($"加油站總數: {stations.Count}");

        // 取前 5 筆資料顯示
        foreach (var station in stations.Take(5))
        {
            Console.WriteLine($"名稱: {station.Name}, 地址: {station.Address}");
        }

        // 進階 LINQ 操作
        Console.WriteLine("\n台北市的加油站數量: " + stations.Count(s => s.Address.Contains("台北市")));

        var sortedStations = stations.OrderBy(s => s.Name).Take(5);
        Console.WriteLine("\n排序後的前 5 個加油站:");
        foreach (var station in stations)
        {
            Console.WriteLine($"名稱: {station.Name}, 地址: {station.Address}");

        }
    }
}

// 物件導向設計：加油站類別
class GasStation
{
    [JsonProperty("站代號")]
    public string? StationId { get; set; }

    [JsonProperty("站名")]
    public string? Name { get; set; }

    [JsonProperty("郵遞區號")]
    public string? ZipCode { get; set; }

    [JsonProperty("地址")]
    public string? Address { get; set; }

    [JsonProperty("電話")]
    public string? Phone { get; set; }

    [JsonProperty("提供服務時段")]
    public string? ServiceHours { get; set; }
}
