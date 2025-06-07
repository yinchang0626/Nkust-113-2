using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;


public class ScreeningData
{
    [Name("統計期")]
    public string? 統計期 { get; set; }

    [Name("篩檢情形別")]
    public string? 篩檢情形別 { get; set; }

    [Name("總計[人]")]
    public int 總計人數 { get; set; }

    [Name("血壓[人]")]
    public int 血壓人數 { get; set; }

    [Name("血糖[人]")]
    public int 血糖人數 { get; set; }

    [Name("血膽固醇[人]")]
    public int 血膽固醇人數 { get; set; }
}

class Program
{
    static void Main()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            MissingFieldFound = null,
            BadDataFound = null
        };

        
        using var reader = new StreamReader("a05041701-2151091053.csv", System.Text.Encoding.UTF8);
        using var csv = new CsvReader(reader, config);

        var records = csv.GetRecords<ScreeningData>().ToList();

        
        Console.WriteLine("📊 歷年篩檢人數統計：");
        foreach (var r in records)
        {
            if (r.篩檢情形別 == "篩檢人數")
            {
                Console.WriteLine($"➡ {r.統計期}：");
                Console.WriteLine($"  總計：{r.總計人數} 人");
                Console.WriteLine($"  血壓：{r.血壓人數} 人，血糖：{r.血糖人數} 人，膽固醇：{r.血膽固醇人數} 人");
                Console.WriteLine();
            }
        }
    }
}