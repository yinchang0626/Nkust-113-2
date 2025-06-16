using System;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string filePath = "wqx_p_06.csv";

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            MissingFieldFound = null,
            BadDataFound = null
        };

        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, config);
        var records = csv.GetRecords<AirQualityRecord>().ToList();

        Console.WriteLine($"資料總筆數：{records.Count}");

        Console.WriteLine("\n前 5 筆資料：");
        foreach (var r in records.Take(5))
        {
            Console.WriteLine($"測站：{r.SiteName}, 縣市：{r.County}, 污染物：{r.Pollutant}, 數值：{r.Concentration}, 發佈時間：{r.PublishTime}");
        }

        var top = records
            .Where(r => double.TryParse(r.Concentration, out _))
            .OrderByDescending(r => double.Parse(r.Concentration))
            .FirstOrDefault();

        if (top != null)
        {
            Console.WriteLine($"\n濃度最高測站：{top.SiteName}（{top.County}） - {top.Concentration} {top.Unit}");
        }
    }
}
