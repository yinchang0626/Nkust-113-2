using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class CwaopendataRoot
{
    public Cwaopendata cwaopendata { get; set; }
}

public class Cwaopendata
{
    public Dataset dataset { get; set; }
}

public class Dataset
{
    public List<Station> Station { get; set; }
}

public class Station
{
    public string StationName { get; set; }
    public string StationId { get; set; }
    public ObsTime ObsTime { get; set; }
    public GeoInfo GeoInfo { get; set; }
    public WeatherElement WeatherElement { get; set; }
}

public class ObsTime
{
    public string DateTime { get; set; }
}

public class GeoInfo
{
    public List<Coordinate> Coordinates { get; set; }
    public string StationAltitude { get; set; }
}

public class Coordinate
{
    public string CoordinateName { get; set; }
    public string CoordinateFormat { get; set; }
    public string StationLatitude { get; set; }
    public string StationLongitude { get; set; }
}

public class WeatherElement
{
    public string SolarRadiation { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        string filePath = @"001.json"; // 請替換為您的 JSON 檔案路徑

        try
        {
            // 讀取 JSON 文件
            string jsonData = File.ReadAllText(filePath);

            // 解析 JSON
            var data = JsonConvert.DeserializeObject<CwaopendataRoot>(jsonData);

            // 檢查是否有 "Station" 資料
            if (data?.cwaopendata?.dataset?.Station != null)
            {
                var stations = data.cwaopendata.dataset.Station;
                Console.WriteLine($"Total number of records資料總筆數: {stations.Count}");

                // 顯示部分資料（最多顯示前 5 筆）
                Console.WriteLine("\nPartial information部分資料:");
                for (int i = 0; i < Math.Min(6, stations.Count); i++)
                {
                    var station = stations[i];
                    Console.WriteLine($"log紀錄 {i + 1}:");
                    Console.WriteLine($"  Site name站名: {station.StationName}");
                    Console.WriteLine($"  Site ID站點 ID: {station.StationId}");
                    Console.WriteLine($"  Observation time觀測時間: {station.ObsTime?.DateTime}");
                    Console.WriteLine($"  altitude海拔高度: {station.GeoInfo?.StationAltitude}");
                    Console.WriteLine($"  Insolation amount日射量: {station.WeatherElement?.SolarRadiation}\n");
                }
            }
            else
            {
                Console.WriteLine("Error: 'Station' data not found錯誤: 找不到 'Station' 資料");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while reading JSON讀取 JSON 時發生錯誤: {ex.Message}");
        }
    }
}
