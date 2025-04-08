using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // CSV 檔案路徑
        var filePath = "臺北市人口離婚情形時間數列統計資料.csv";

        // 讀取 CSV 檔案
        var records = ReadCsv(filePath);

        // 顯示資料總筆數
        Console.WriteLine($"資料總筆數: {records.Count()}");

        // 顯示部分關鍵欄位
        foreach (var record in records.Take(5)) // 顯示前五筆資料
        {
            Console.WriteLine($"統計期: {record.統計期}, 登記/發生日期: {record.登記發生日期}, 離婚對數總計: {record.離婚對數總計}");
        }

        // 使用 LINQ 進行統計分析：顯示最大、最小離婚對數
        var maxDivorceCount = records.Max(r => r.離婚對數總計);
        var minDivorceCount = records.Min(r => r.離婚對數總計);
        Console.WriteLine($"最大離婚對數: {maxDivorceCount}, 最小離婚對數: {minDivorceCount}");

        // 顯示平均離婚年齡（男）
        var avgAgeMale = records.Average(r => r.平均離婚年齡男);
        Console.WriteLine($"平均離婚年齡（男）：{avgAgeMale}");
    }

    // 讀取 CSV 檔案並解析
    static IEnumerable<DivorceRecord> ReadCsv(string filePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true, // 設定有標頭行
            Delimiter = ",", // 設定分隔符號，如果是 CSV 檔案通常是逗號
            HeaderValidated = null // 忽略標頭驗證
        };

        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, config); // 使用設定
        csv.Context.RegisterClassMap<DivorceRecordMap>(); // 註冊 ClassMap 進行欄位映射
        var records = csv.GetRecords<DivorceRecord>().ToList();
        return records;
    }
}