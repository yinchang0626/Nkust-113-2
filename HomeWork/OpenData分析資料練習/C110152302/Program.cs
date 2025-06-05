using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using CsvHelper;

class Program
{
    static void Main(string[] args)
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        string filePath = "114-1年桃園市列冊獨居老人服務概況.csv";

        using var reader = new StreamReader(filePath, System.Text.Encoding.GetEncoding("big5"));
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<ElderCareRecord>().ToList();

        Console.WriteLine($" 資料總筆數：{records.Count} 筆\n");

        Console.WriteLine(" 前 5 筆行政區資料：");
        foreach (var r in records.Take(5))
        {
            Console.WriteLine($" 區域：{r.Area}, 電話問安：{r.PhoneGreetings}, 長照服務：{r.LongTermCare}, 服務合計：{r.TotalService}");
        }

        var topService = records.OrderByDescending(r => r.TotalService).First();
        Console.WriteLine($"\n 服務最多區域：{topService.Area}，共 {topService.TotalService} 次");

        var over30k = records.Where(r => r.PhoneGreetings > 30000).Select(r => r.Area);
        Console.WriteLine("\n 電話問安超過 3 萬的區域有：");
        foreach (var area in over30k)
            Console.WriteLine($"- {area}");

        var avgPerPerson = records.Average(r => (double)r.TotalService / r.People);
        Console.WriteLine($"\n 平均每人獲得服務次數：約 {avgPerPerson:F2} 次");
    }
}
