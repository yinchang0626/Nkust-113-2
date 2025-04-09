using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

class Program
{
    static void Main(string[] args)
    {
        var filePath = "臺北市役男徵兵檢查BMI值統計.csv"; // CSV 路徑

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            HeaderValidated = null,
            MissingFieldFound = null
        };

        // 嘗試使用 UTF-8 編碼來讀取文件
        using var reader = new StreamReader(filePath, Encoding.UTF8); // 改為 UTF-8
        using var csv = new CsvReader(reader, config);

        // 使用 ClassMap 映射 CSV 列與 BmiRecord 類的屬性
        csv.Context.RegisterClassMap<BmiRecordMap>();

        var records = csv.GetRecords<BmiRecord>().ToList();

        // 篩選：只要113年資料（注意：年是 string）
        var year113 = records.Where(r => r.年 == "113").ToList();

        Console.WriteLine($"113年資料總筆數：{year113.Count}");

        // 顯示每筆記錄
        //foreach (var r in year113)
        //{
        //    Console.WriteLine($"{r.年}年{r.月}月 | BMI分類: {r.BMI值體位} | 體位: {r.體位} | 人數: {r.人數} | 百分比: {r.百分比}");
        //}
        // 顯示隨機選取的20筆記錄
        var random20 = year113.OrderBy(r => Guid.NewGuid()).Take(20);

        foreach (var r in random20)
        {
            Console.WriteLine($"{r.年}年{r.月}月 | BMI分類: {r.BMI值體位} | 體位: {r.體位} | 人數: {r.人數} | 百分比: {r.百分比}");
        }
        // 額外分析：總人數
        var total = year113.Sum(r => r.人數);
        Console.WriteLine($"113年總人數：{total}");

        // 計算平均人數
        var average = year113.Average(r => r.人數);
        Console.WriteLine($"113年平均人數：{average}");

        // 計算最大和最小人數
        var max = year113.Max(r => r.人數);
        var min = year113.Min(r => r.人數);
        Console.WriteLine($"113年最大人數：{max}");
        Console.WriteLine($"113年最小人數：{min}");

        // 按人數排序並顯示前五筆
        var top5 = year113.OrderByDescending(r => r.人數).Take(5);
        Console.WriteLine("113年人數最多的前五筆記錄：");
        foreach (var r in top5)
        {
            Console.WriteLine($"{r.年}年{r.月}月 | BMI分類: {r.BMI值體位} | 體位: {r.體位} | 人數: {r.人數} | 百分比: {r.百分比}");
        }

        // 計算特定 BMI 分類的總人數（例如 BMI值體位 = "過重"）
        var overWeight = year113.Where(r => r.BMI值體位 == "過重").Sum(r => r.人數);
        Console.WriteLine($"113年過重的總人數：{overWeight}");
    }
}