using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration;
using System.Formats.Asn1;
using CsvHelper.Configuration.Attributes;
using CsvHelper.Configuration.Attributes;
namespace VillageHeadStats
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string filePath = "桃園市(縣)村里長當選人人數.csv";
            var records = ReadCsv(filePath);

            Console.WriteLine($"資料總筆數：{records.Count}");

            Console.WriteLine("\n前5筆資料（屆別、男性、女性）：");
            foreach (var record in records.Take(5))
            {
                Console.WriteLine($"{record.Term} - 男性: {record.Male}, 女性: {record.Female}");
            }

            Console.WriteLine("\n=== 統計資訊 ===");
            Console.WriteLine($"男性總數：{records.Sum(r => r.Male)}");
            Console.WriteLine($"女性總數：{records.Sum(r => r.Female)}");
            Console.WriteLine($"男女總數平均（每屆）：{records.Average(r => r.Male + r.Female):0.00}");
            Console.WriteLine($"女性比例最高的屆別：{records.OrderByDescending(r => r.Female / (double)(r.Male + r.Female)).First().Term}");
        }

        public static List<VillageHeadRecord> ReadCsv(string path)
        {
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            });

            return csv.GetRecords<VillageHeadRecord>().ToList();
        }
    }


public class VillageHeadRecord
    {
        [Name("屆別")]
        public string Term { get; set; }

        [Name("男性")]
        public int Male { get; set; }

        [Name("女性")]
        public int Female { get; set; }
    }
}
