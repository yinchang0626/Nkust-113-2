using System;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

class MarketData
{
    [JsonPropertyName("marketID")]
    public string 市場ID { get; set; }

    [JsonPropertyName("shortName")]
    public string 縣市 { get; set; }

    [JsonPropertyName("name")]
    public string 名稱 { get; set; }

    [JsonPropertyName("transDate")]
    public string 交易日期 { get; set; }

    [JsonPropertyName("nextTransDate")]
    public string 下次交易日期 { get; set; }

    [JsonPropertyName("inQuantity")]
    public string 進貨量 { get; set; }

    [JsonPropertyName("nextInQuantity")]
    public string 下次進貨量 { get; set; }

    [JsonPropertyName("killQuantity")]
    public string 屠宰量 { get; set; }
}

class Program
{
    static void Main()
    {
        string fileName = "123.JSON";
        string currentDir = Directory.GetCurrentDirectory();
        string filePath = Path.Combine(currentDir, fileName);

        if (!File.Exists(filePath))
        {
            string projectDir = Directory.GetParent(currentDir)?.Parent?.Parent?.FullName;
            if (projectDir != null)
            {
                filePath = Path.Combine(projectDir, fileName);
            }
        }

        if (!File.Exists(filePath))
        {
            var allFiles = Directory.GetFiles(Directory.GetParent(currentDir).FullName, "*.JSON", SearchOption.AllDirectories);
            filePath = allFiles.FirstOrDefault(f => Path.GetFileName(f).Equals(fileName, StringComparison.OrdinalIgnoreCase));
        }

        if (filePath == null || !File.Exists(filePath))
        {
            Console.WriteLine("找不到 JSON 檔案: " + fileName);
            return;
        }

        Console.WriteLine("找到 JSON 檔案: " + filePath);
        ProcessJson(filePath);
    }

    static void ProcessJson(string filePath)
    {
        try
        {
            string jsonData = File.ReadAllText(filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = null // 確保屬性名稱不被更改
            };

            var items = JsonSerializer.Deserialize<List<MarketData>>(jsonData, options);

            if (items == null || items.Count == 0)
            {
                Console.WriteLine("JSON 解析失敗或內容為空！");
                return;
            }

            DisplayData(items);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JSON 解析失敗: {ex.Message}");
        }
    }

    static void DisplayData(List<MarketData> data)
    {
        Console.WriteLine($"總筆數: {data.Count}\n");

        foreach (var item in data)
        {
            Console.WriteLine($"市場ID: {item.市場ID}");
            Console.WriteLine($"縣市: {item.縣市}");
            Console.WriteLine($"名稱: {item.名稱}");
            Console.WriteLine($"交易日期: {item.交易日期}");
            Console.WriteLine($"下次交易日期: {item.下次交易日期}");
            Console.WriteLine($"進貨量: {item.進貨量}");
            Console.WriteLine($"下次進貨量: {item.下次進貨量}");
            Console.WriteLine($"屠宰量: {item.屠宰量}");
            Console.WriteLine("\n------------------------\n");
        }
    }
}