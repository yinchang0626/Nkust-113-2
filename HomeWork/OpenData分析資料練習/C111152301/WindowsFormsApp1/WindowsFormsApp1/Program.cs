using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using System.Windows.Forms.VisualStyles;
using WindowsFormsApp1;
using System.Reflection.Emit;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;

public struct Data
{
    public List<int> G42_num, G42_year, G42_month, G42_aveprice,
                     G21_num, G21_year, G21_month, G21_aveprice,
                     G41_num, G41_year, G41_month, G41_aveprice,
                     G32_num, G32_year, G32_month, G32_aveprice;
}
public struct Data_count{
    int year, month, num;
}
public struct Data_cal
{
    public List<double> average_G42, average_G21, average_G41, average_G32;
    public List<int> G42_max, G21_max, G41_max, G32_max,
                     G42_min, G21_min, G41_min, G32_min;
}

namespace WindowsFormsApp1
{
    internal static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 呼叫 Load 方法來讀取和分類 CSV 數據
            var(changhua_data, yunlin_data) = Load("../../sheep.csv");
            var (changhua_month_G42, changhua_month_G21, changhua_month_G41, changhua_month_G32) = count();
            Data_cal changhua, yunlin;
            (changhua.G42_max, changhua.G42_min, changhua.average_G42) = PrintStatistics(changhua, "閹公羊販賣數量");
            (changhua.G21_max, changhua.G21_min, changhua.average_G21) = PrintStatistics(changhua, "女羊販賣數量");
            (changhua.G41_max, changhua.G41_min, changhua.average_G41) = PrintStatistics(changhua, "努比亞雜交羊販賣數量");
            (changhua.G32_max, changhua.G32_min, changhua.average_G32) = PrintStatistics(changhua, "規格外羊販賣數量");

            (yunlin.G42_max, yunlin.G42_min, yunlin.average_G42) = PrintStatistics(yunlin_data, "閹公羊販賣數量");
            (yunlin.G21_max, yunlin.G21_min, yunlin.average_G21) = PrintStatistics(yunlin_data, "女羊販賣數量");
            (yunlin.G41_max, yunlin.G41_min, yunlin.average_G41) = PrintStatistics(yunlin_data, "努比亞雜交羊販賣數量");
            (yunlin.G32_max, yunlin.G32_min, yunlin.average_G32) = PrintStatistics(yunlin_data, "規格外羊販賣數量");
  
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1()); // 啟動 Form1
        }
        static (Data, Data) Load(string filePath)
        {
            // 註冊編碼提供程式以支援 Big5 編碼
            

            // 使用 Big5 編碼讀取 CSV 檔案內容
            string csvContent = File.ReadAllText(filePath, Encoding.GetEncoding("Big5"));
            List<int> changhua_G42_num = new List<int>();
            List<int> changhua_G42_year = new List<int>();
            List<int> changhua_G42_month = new List<int>();
            List<int> changhua_G42_aveprice = new List<int>();
            List<int> changhua_G21_num = new List<int>();
            List<int> changhua_G21_year = new List<int>();
            List<int> changhua_G21_month = new List<int>();
            List<int> changhua_G21_aveprice = new List<int>();
            List<int> changhua_G41_num = new List<int>();
            List<int> changhua_G41_year = new List<int>();
            List<int> changhua_G41_month = new List<int>();
            List<int> changhua_G41_aveprice = new List<int>();
            List<int> changhua_G32_num = new List<int>();
            List<int> changhua_G32_year = new List<int>();
            List<int> changhua_G32_month = new List<int>();
            List<int> changhua_G32_aveprice = new List<int>();
            List<int> yunlin_G42_num = new List<int>();
            List<int> yunlin_G42_year = new List<int>();
            List<int> yunlin_G42_month = new List<int>();
            List<int> yunlin_G42_aveprice = new List<int>();
            List<int> yunlin_G21_num = new List<int>();
            List<int> yunlin_G21_year = new List<int>();
            List<int> yunlin_G21_month = new List<int>();
            List<int> yunlin_G21_aveprice = new List<int>();
            List<int> yunlin_G41_num = new List<int>();
            List<int> yunlin_G41_year = new List<int>();
            List<int> yunlin_G41_month = new List<int>();
            List<int> yunlin_G41_aveprice = new List<int>();
            List<int> yunlin_G32_num = new List<int>();
            List<int> yunlin_G32_year = new List<int>();
            List<int> yunlin_G32_month = new List<int>();
            List<int> yunlin_G32_aveprice = new List<int>();
            Data data_changhua, data_yunlin;
            // 使用 TextFieldParser 從字串中解析 CSV 內容
            using (TextReader reader = new StringReader(csvContent))
            using (TextFieldParser parser = new TextFieldParser(reader))
            {
                parser.TextFieldType = FieldType.Delimited; // 設定為分隔檔案
                parser.SetDelimiters(","); // 設定分隔符號為逗號

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields(); // 讀取每一行的字段
                    string date = fields[3];
                    string[] sdate = date.Split(new string[] { "/"}, StringSplitOptions.RemoveEmptyEntries);
                    if (fields[1].Contains("雲林縣"))      // 儲存符合條件的資料
                    {
                        if (fields[4].Contains("G42"))
                        {
                            yunlin_G42_num.Add(int.Parse(fields[7]));
                            yunlin_G42_year.Add(int.Parse(fields[0]));
                            yunlin_G42_month.Add(int.Parse(fields[1]));
                            yunlin_G42_aveprice.Add(int.Parse(fields[9]));
                        }
                        else if (fields[4].Contains("G21"))
                        {
                            yunlin_G21_num.Add(int.Parse(fields[7]));
                            yunlin_G21_year.Add(int.Parse(fields[0]));
                            yunlin_G21_month.Add(int.Parse(fields[1]));
                            yunlin_G21_aveprice.Add(int.Parse(fields[9]));
                        }
                        else if (fields[4].Contains("G41"))
                        {
                            yunlin_G41_num.Add(int.Parse(fields[7]));
                            yunlin_G41_year.Add(int.Parse(fields[0]));
                            yunlin_G41_month.Add(int.Parse(fields[1]));
                            yunlin_G41_aveprice.Add(int.Parse(fields[9]));
                        }
                        else if (fields[4].Contains("G32"))
                        {
                            yunlin_G32_num.Add(int.Parse(fields[7]));
                            yunlin_G32_year.Add(int.Parse(fields[0]));
                            yunlin_G32_month.Add(int.Parse(fields[1]));
                            yunlin_G32_aveprice.Add(int.Parse(fields[9]));
                        }
                    }

                    if (fields[1].Contains("彰化縣"))
                    {
                        if (fields[4].Contains("G42"))
                        {
                            changhua_G42_num.Add(int.Parse(fields[7]));
                            changhua_G42_year.Add(int.Parse(fields[0]));
                            changhua_G42_month.Add(int.Parse(fields[1]));
                            changhua_G42_aveprice.Add(int.Parse(fields[9]));
                        }
                        else if (fields[4].Contains("G21"))
                        {
                            changhua_G21_num.Add(int.Parse(fields[7]));
                            changhua_G21_year.Add(int.Parse(fields[0]));
                            changhua_G21_month.Add(int.Parse(fields[1]));
                            changhua_G21_aveprice.Add(int.Parse(fields[9]));
                        }
                        else if (fields[4].Contains("G41"))
                        {
                            changhua_G41_num.Add(int.Parse(fields[7]));
                            changhua_G41_year.Add(int.Parse(fields[0]));
                            changhua_G41_month.Add(int.Parse(fields[1]));
                            changhua_G41_aveprice.Add(int.Parse(fields[9]));
                        }
                        else if (fields[4].Contains("G32"))
                        {
                            changhua_G32_num.Add(int.Parse(fields[7]));
                            changhua_G32_year.Add(int.Parse(fields[0]));
                            changhua_G32_month.Add(int.Parse(fields[1]));
                            changhua_G32_aveprice.Add(int.Parse(fields[9]));
                        }
                    }
                }
            }
            data_changhua.G42_num = changhua_G42_num;
            data_changhua.G42_year = changhua_G42_year;
            data_changhua.G42_month = changhua_G42_month;
            data_changhua.G42_aveprice = changhua_G42_aveprice;
            data_changhua.G21_num = changhua_G21_num;
            data_changhua.G21_year = changhua_G21_year;
            data_changhua.G21_month = changhua_G21_month;
            data_changhua.G21_aveprice = changhua_G21_aveprice;
            data_changhua.G41_num = changhua_G41_num;
            data_changhua.G41_year = changhua_G41_year;
            data_changhua.G41_month = changhua_G41_month;
            data_changhua.G41_aveprice = changhua_G41_aveprice;
            data_changhua.G32_num = changhua_G32_num;
            data_changhua.G32_year = changhua_G32_year;
            data_changhua.G32_month = changhua_G32_month;
            data_changhua.G32_aveprice = changhua_G32_aveprice;
            data_yunlin.G42_num = yunlin_G42_num;
            data_yunlin.G42_year = yunlin_G42_year;
            data_yunlin.G42_month = yunlin_G42_month;
            data_yunlin.G42_aveprice = yunlin_G42_aveprice;
            data_yunlin.G21_num = yunlin_G21_num;
            data_yunlin.G21_year = yunlin_G21_year;
            data_yunlin.G21_month = yunlin_G21_month;
            data_yunlin.G21_aveprice = yunlin_G21_aveprice;
            data_yunlin.G41_num = yunlin_G41_num;
            data_yunlin.G41_year = yunlin_G41_year;
            data_yunlin.G41_month = yunlin_G41_month;
            data_yunlin.G41_aveprice = yunlin_G41_aveprice;
            data_yunlin.G32_num = yunlin_G32_num;
            data_yunlin.G32_year = yunlin_G32_year;
            data_yunlin.G32_month = yunlin_G32_month;
            data_yunlin.G32_aveprice = yunlin_G32_aveprice;
            return (data_changhua, data_yunlin);
        }


        static (int, int, int, double) PrintStatistics(List<int> data, string label)
        {

            double average = CalculateAverage(data);
            var (max, max_year) = CalculateMax(data);
            var (min, min_year) = CalculateMin(data);

            Console.WriteLine($"{label}");
            Console.WriteLine($"最大值: {max}, 年份: {max_year}");
            Console.WriteLine($"最小值: {min}, 年份: {min_year}");
            Console.WriteLine($"平均值: {average}");

            return (max, max, min, min_year, average);
        }

        //統計月份羊隻數量
        static ()

        // 計算平均值
        static double CalculateAverage(List<int> data)
        {
            double total = 0;
            foreach (var value in data)
            {
                total += value;
            }
            return Math.Round(total / data.Count);
        }

        // 計算最大值
        static (int, int) CalculateMax(List<int> data)
        {
            int max = int.MinValue;
            int year = 0;

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i] > max)
                {
                    max = data[i];
                    year = i + 57;
                }
            }
            return (max, year);
        }


        // 計算最小值
        static (int, int) CalculateMin(List<int> data)
        {
            int min = int.MaxValue;
            int year = 0;

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i] < min)
                {
                    min = data[i];
                    year = i + 57;
                }
            }
            return (min, year);
        }
    }
}