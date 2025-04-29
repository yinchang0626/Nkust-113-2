using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovernmentOpenDataAnalysis
{

    class Program
    {
        public class Entity
        {
            public int Num { get; set; }
            public DateTime Date { get; set; }
            public string Text { get; set; }
            public bool Flag { get; set; }

            public Entity(int num, DateTime date, string text, bool flag)
            {
                Num = num;
                Date = date;
                Text = text;
                Flag = flag;
            }
            public Entity() { } // CsvHelper需要無參數建構式
        }

        static Entity[] TestData = new Entity[]
        {
            new Entity(1, new DateTime(2012,12,21), "Normal", true),
            new Entity(16, new DateTime(2012,12,21), "Taipei, Taiwan", true),
            new Entity(32, new DateTime(2012,12,21), $@"""雙引號""跟,都來一下
換行當然不可少
", false)
        };

        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Directory.CreateDirectory("output");

            string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            string dataFilePath = Path.Combine(projectRoot, "data", "echnology-Based Law Enforcement in Kaohsiung City.csv");

            PrintCsvAsTable(dataFilePath);           // ✅ 表格顯示
            CsvToDynamicList(dataFilePath); // ✅ 動態讀取 CSV 並印出 JSON
            ParseAsStrArray(dataFilePath);  // ✅ 字串陣列解析
        }


        static string SelectCsvFile()
        {
            string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            string dataDir = Path.Combine(projectRoot, "data");

            if (!Directory.Exists(dataDir))
            {
                Console.WriteLine("找不到資料夾：" + dataDir);
                return null;
            }

            var csvFiles = Directory.GetFiles(dataDir, "*.csv");
            if (csvFiles.Length == 0)
            {
                Console.WriteLine("找不到任何 .csv 檔案。");
                return null;
            }

            Console.WriteLine("請選擇要讀取的 CSV 檔案：");
            for (int i = 0; i < csvFiles.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(csvFiles[i])}");
            }

            Console.Write("輸入編號：");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= csvFiles.Length)
            {
                return csvFiles[choice - 1];
            }

            Console.WriteLine("輸入錯誤。");
            return null;
        }

        //物件陣列轉成 CSV
        static void ObjArrayToCsv()
        {
            using (var sw = new StreamWriter("C:\\Users\\mark9\\source\\repos\\GovernmentOpenDataAnalysis\\GovernmentOpenDataAnalysis\\data\\echnology-Based Law Enforcement in Kaohsiung City.csv", false, Encoding.UTF8))
            {
                using (var csv = new CsvWriter(sw, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(TestData);
                }
            }
        }
        // CSV 還原成物件陣列
        static void CsvToObjArray(string dataFilePath)
        {
            using var sr = new StreamReader("C:\\Users\\mark9\\source\\repos\\GovernmentOpenDataAnalysis\\GovernmentOpenDataAnalysis\\data\\echnology-Based Law Enforcement in Kaohsiung City.csv", Encoding.UTF8);
            using var csv = new CsvReader(sr, CultureInfo.InvariantCulture);
            var data = csv.GetRecords<Entity>().ToList();
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(data,
                new System.Text.Json.JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                }));
            Console.WriteLine();
        }
        // 不使用強型別，解析成 string 陣列
        static void ParseAsStrArray(string dataFilePath)
        {
            string[] propNames = null;
            List<string[]> rows = new List<string[]>();

            using var sr = new StreamReader("C:\\Users\\mark9\\source\\repos\\GovernmentOpenDataAnalysis\\GovernmentOpenDataAnalysis\\data\\echnology-Based Law Enforcement in Kaohsiung City.csv", Encoding.UTF8);
            using var csv = new CsvReader(sr, CultureInfo.InvariantCulture);
            while (csv.Read())
            {
                string[] strArray = Enumerable.Range(0, csv.Parser.Count)
                    .Select(i => csv.GetField(i)).ToArray();
                if (propNames == null)
                    propNames = strArray;
                else
                    rows.Add(strArray);
            }
            Console.WriteLine($"PropNames={string.Join(",", propNames)}");
            for (int r = 0; r < rows.Count; r++)
            {
                var cells = rows[r];
                for (int c = 0; c < cells.Length; c++)
                {
                    Console.WriteLine($"[{r},{c}]={cells[c]}");
                }
            }
            Console.WriteLine();
        }
        static void PrintCsvAsTable(string dataFilePath)
        {
            Console.WriteLine("===== 表格格式顯示 CSV 內容 =====");
            string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            //string dataFilePath = Path.Combine(projectRoot, "data", "echnology-Based Law Enforcement in Kaohsiung City.csv");

            using var reader = new StreamReader(dataFilePath, Encoding.UTF8);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var allRows = new List<string[]>();
            while (csv.Read())
            {
                var row = Enumerable.Range(0, csv.Parser.Count)
                    .Select(i => csv.GetField(i))
                    .ToArray();
                allRows.Add(row);
            }

            if (allRows.Count == 0)
            {
                Console.WriteLine("CSV 檔案為空。");
                return;
            }

            int colCount = allRows[0].Length;
            int[] colWidths = new int[colCount];

            // 計算每欄最大寬度
            foreach (var row in allRows)
            {
                for (int i = 0; i < colCount; i++)
                {
                    var cell = row[i]?.Replace("\r", "").Replace("\n", "\\n") ?? "";
                    colWidths[i] = Math.Max(colWidths[i], cell.Length);
                }
            }

            // 印出橫線
            void PrintLine()
            {
                Console.WriteLine("+" + string.Join("+", colWidths.Select(w => new string('-', w + 2))) + "+");
            }

            // 印出一列
            void PrintRow(string[] row)
            {
                Console.WriteLine("| " + string.Join(" | ", row.Select((cell, i) =>
                {
                    cell = cell?.Replace("\r", "").Replace("\n", "\\n") ?? "";
                    return cell.PadRight(colWidths[i]);
                })) + " |");
            }

            PrintLine();
            PrintRow(allRows[0]); // header
            PrintLine();
            foreach (var row in allRows.Skip(1))
            {
                PrintRow(row);
            }
            PrintLine();

            Console.WriteLine("===== 結束 =====\n");
        }
        static void CsvToDynamicList(string dataFilePath)
        {
            using var sr = new StreamReader(dataFilePath, Encoding.UTF8);
            using var csv = new CsvReader(sr, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<dynamic>().ToList();

            string json = System.Text.Json.JsonSerializer.Serialize(
                records,
                new System.Text.Json.JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

            Console.WriteLine("===== 自動對應欄位顯示 CSV 內容（dynamic） =====");
            Console.WriteLine(json);
            Console.WriteLine("===== 結束 =====\n");
        }


    }

}