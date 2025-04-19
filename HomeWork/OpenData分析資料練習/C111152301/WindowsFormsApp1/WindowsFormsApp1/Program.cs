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
using System.Reflection;
using System.Runtime.InteropServices;

public struct Data
{
    public List<double> G42_aveprice, G21_aveprice, G41_aveprice, G32_aveprice;
    public List<int> G42_num, G42_year, G42_month,
                     G21_num, G21_year, G21_month,
                     G41_num, G41_year, G41_month,
                     G32_num, G32_year, G32_month;
}
public struct Data_count{
    public int year, month, num, count, avenum;
    public double price, aveprice;
}
public struct Data_cal
{
    public double average_G42, average_G21, average_G41, average_G32,
                  G42_max, G21_max, G41_max, G32_max,
                  G42_min, G21_min, G41_min, G32_min;
    public int G42_max_year, G21_max_year, G41_max_year, G32_max_year,
               G42_max_month, G21_max_month, G41_max_month, G32_max_month,
               G42_min_year, G21_min_year, G41_min_year, G32_min_year,
               G42_min_month, G21_min_month, G41_min_month, G32_min_month;
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
            var (changhua_month_G42, changhua_month_G21, changhua_month_G41, changhua_month_G32) = Count(changhua_data);
            var (yunlin_month_G42, yunlin_month_G21, yunlin_month_G41, yunlin_month_G32) = Count(yunlin_data);
            Data_cal changhua_num, yunlin_num, changhua_price, yunlin_price;
            (changhua_num.G42_max, changhua_num.G42_max_year, changhua_num.G42_max_month, changhua_num.G42_min, changhua_num.G42_min_year, changhua_num.G42_min_month, changhua_num.average_G42) = Cal_num(changhua_month_G42);
            (changhua_num.G21_max, changhua_num.G21_max_year, changhua_num.G21_max_month, changhua_num.G21_min, changhua_num.G21_min_year, changhua_num.G21_min_month, changhua_num.average_G21) = Cal_num(changhua_month_G21);
            (changhua_num.G41_max, changhua_num.G41_max_year, changhua_num.G41_max_month, changhua_num.G41_min, changhua_num.G41_min_year, changhua_num.G41_min_month, changhua_num.average_G41) = Cal_num(changhua_month_G41);
            (changhua_num.G32_max, changhua_num.G32_max_year, changhua_num.G32_max_month, changhua_num.G32_min, changhua_num.G32_min_year, changhua_num.G32_min_month, changhua_num.average_G32) = Cal_num(changhua_month_G32);
            (yunlin_num.G42_max, yunlin_num.G42_max_year, yunlin_num.G42_max_month, yunlin_num.G42_min, yunlin_num.G42_min_year, yunlin_num.G42_min_month, yunlin_num.average_G42) = Cal_num(yunlin_month_G42);
            (yunlin_num.G21_max, yunlin_num.G21_max_year, yunlin_num.G21_max_month, yunlin_num.G21_min, yunlin_num.G21_min_year, yunlin_num.G21_min_month, yunlin_num.average_G21) = Cal_num(yunlin_month_G21);
            (yunlin_num.G41_max, yunlin_num.G41_max_year, yunlin_num.G41_max_month, yunlin_num.G41_min, yunlin_num.G41_min_year, yunlin_num.G41_min_month, yunlin_num.average_G41) = Cal_num(yunlin_month_G41);
            (yunlin_num.G32_max, yunlin_num.G32_max_year, yunlin_num.G32_max_month, yunlin_num.G32_min, yunlin_num.G32_min_year, yunlin_num.G32_min_month, yunlin_num.average_G32) = Cal_num(yunlin_month_G32);
           
            (changhua_price.G42_max, changhua_price.G42_max_year, changhua_price.G42_max_month, changhua_price.G42_min, changhua_price.G42_min_year, changhua_price.G42_min_month, changhua_price.average_G42) = Cal_price(changhua_month_G42);
            (changhua_price.G21_max, changhua_price.G21_max_year, changhua_price.G21_max_month, changhua_price.G21_min, changhua_price.G21_min_year, changhua_price.G21_min_month, changhua_price.average_G21) = Cal_price(changhua_month_G21);
            (changhua_price.G41_max, changhua_price.G41_max_year, changhua_price.G41_max_month, changhua_price.G41_min, changhua_price.G41_min_year, changhua_price.G41_min_month, changhua_price.average_G41) = Cal_price(changhua_month_G41);
            (changhua_price.G32_max, changhua_price.G32_max_year, changhua_price.G32_max_month, changhua_price.G32_min, changhua_price.G32_min_year, changhua_price.G32_min_month, changhua_price.average_G32) = Cal_price(changhua_month_G32);
            (yunlin_price.G42_max, yunlin_price.G42_max_year, yunlin_price.G42_max_month, yunlin_price.G42_min, yunlin_price.G42_min_year, yunlin_price.G42_min_month, yunlin_price.average_G42) = Cal_price(yunlin_month_G42);
            (yunlin_price.G21_max, yunlin_price.G21_max_year, yunlin_price.G21_max_month, yunlin_price.G21_min, yunlin_price.G21_min_year, yunlin_price.G21_min_month, yunlin_price.average_G21) = Cal_price(yunlin_month_G21);
            (yunlin_price.G41_max, yunlin_price.G41_max_year, yunlin_price.G41_max_month, yunlin_price.G41_min, yunlin_price.G41_min_year, yunlin_price.G41_min_month, yunlin_price.average_G41) = Cal_price(yunlin_month_G41);
            (yunlin_price.G32_max, yunlin_price.G32_max_year, yunlin_price.G32_max_month, yunlin_price.G32_min, yunlin_price.G32_min_year, yunlin_price.G32_min_month, yunlin_price.average_G32) = Cal_price(yunlin_month_G32);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(changhua_month_G42, changhua_month_G21, changhua_month_G41, changhua_month_G32, yunlin_month_G42, yunlin_month_G21,
                                      yunlin_month_G41, yunlin_month_G32, changhua_num, yunlin_num, changhua_price, yunlin_price)); // 啟動 Form1
        }
        static (Data, Data) Load(string filePath)
        {
            // 註冊編碼提供程式以支援 Big5 編碼
            

            // 使用 Big5 編碼讀取 CSV 檔案內容
                string csvContent = File.ReadAllText(filePath, Encoding.GetEncoding("Big5"));
            List<int> changhua_G42_num = new List<int>();
            List<int> changhua_G42_year = new List<int>();
            List<int> changhua_G42_month = new List<int>();
            List<double> changhua_G42_aveprice = new List<double>();
            List<int> changhua_G21_num = new List<int>();
            List<int> changhua_G21_year = new List<int>();
            List<int> changhua_G21_month = new List<int>();
            List<double> changhua_G21_aveprice = new List<double>();
            List<int> changhua_G41_num = new List<int>();
            List<int> changhua_G41_year = new List<int>();
            List<int> changhua_G41_month = new List<int>();
            List<double> changhua_G41_aveprice = new List<double>();
            List<int> changhua_G32_num = new List<int>();
            List<int> changhua_G32_year = new List<int>();
            List<int> changhua_G32_month = new List<int>();
            List<double> changhua_G32_aveprice = new List<double>();
            List<int> yunlin_G42_num = new List<int>();
            List<int> yunlin_G42_year = new List<int>();
            List<int> yunlin_G42_month = new List<int>();
            List<double> yunlin_G42_aveprice = new List<double>();
            List<int> yunlin_G21_num = new List<int>();
            List<int> yunlin_G21_year = new List<int>();
            List<int> yunlin_G21_month = new List<int>();
            List<double> yunlin_G21_aveprice = new List<double>();
            List<int> yunlin_G41_num = new List<int>();
            List<int> yunlin_G41_year = new List<int>();
            List<int> yunlin_G41_month = new List<int>();
            List<double> yunlin_G41_aveprice = new List<double>();
            List<int> yunlin_G32_num = new List<int>();
            List<int> yunlin_G32_year = new List<int>();
            List<int> yunlin_G32_month = new List<int>();
            List<double> yunlin_G32_aveprice = new List<double>();
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
                            yunlin_G42_year.Add(int.Parse(sdate[0]));
                            yunlin_G42_month.Add(int.Parse(sdate[1]));
                            yunlin_G42_aveprice.Add(int.Parse(fields[9]));
                        }
                        else if (fields[4].Contains("G21"))
                        {
                            yunlin_G21_num.Add(int.Parse(fields[7]));
                            yunlin_G21_year.Add(int.Parse(sdate[0]));
                            yunlin_G21_month.Add(int.Parse(sdate[1]));
                            yunlin_G21_aveprice.Add(int.Parse(fields[9]));
                        }
                        else if (fields[4].Contains("G41"))
                        {
                            yunlin_G41_num.Add(int.Parse(fields[7]));
                            yunlin_G41_year.Add(int.Parse(sdate[0]));
                            yunlin_G41_month.Add(int.Parse(sdate[1]));
                            yunlin_G41_aveprice.Add(int.Parse(fields[9]));
                        }
                        else if (fields[4].Contains("G32"))
                        {
                            yunlin_G32_num.Add(int.Parse(fields[7]));
                            yunlin_G32_year.Add(int.Parse(sdate[0]));
                            yunlin_G32_month.Add(int.Parse(sdate[1]));
                            yunlin_G32_aveprice.Add(int.Parse(fields[9]));
                        }
                    }

                    if (fields[1].Contains("彰化縣"))
                    {
                        if (fields[4].Contains("G42"))
                        {
                            changhua_G42_num.Add(int.Parse(fields[7]));
                            changhua_G42_year.Add(int.Parse(sdate[0]));
                            changhua_G42_month.Add(int.Parse(sdate[1]));
                            changhua_G42_aveprice.Add(double.Parse(fields[9]));
                        }
                        else if (fields[4].Contains("G21"))
                        {
                            changhua_G21_num.Add(int.Parse(fields[7]));
                            changhua_G21_year.Add(int.Parse(sdate[0]));
                            changhua_G21_month.Add(int.Parse(sdate[1]));
                            changhua_G21_aveprice.Add(double.Parse(fields[9]));
                        }
                        else if (fields[4].Contains("G41"))
                        {
                            changhua_G41_num.Add(int.Parse(fields[7]));
                            changhua_G41_year.Add(int.Parse(sdate[0]));
                            changhua_G41_month.Add(int.Parse(sdate[1]));
                            changhua_G41_aveprice.Add(double.Parse(fields[9]));
                        }
                        else if (fields[4].Contains("G32"))
                        {
                            changhua_G32_num.Add(int.Parse(fields[7]));
                            changhua_G32_year.Add(int.Parse(sdate[0]));
                            changhua_G32_month.Add(int.Parse(sdate[1]));
                            changhua_G32_aveprice.Add(double.Parse(fields[9]));
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

        //統計月份羊隻數量
        static (Data_count[], Data_count[], Data_count[], Data_count[]) Count (Data data)
        {
            Data_count[] G42 = new Data_count[120];
            Data_count[] G21 = new Data_count[120]; 
            Data_count[] G41 = new Data_count[120]; 
            Data_count[] G32 = new Data_count[120];
            for (int i = 0; i < 120; i++)
            {
                G42[i].num = 0;
                G42[i].price = 0;
                G42[i].count = 0;
                G21[i].num = 0;
                G21[i].price = 0;
                G21[i].count = 0;
                G41[i].num = 0;
                G41[i].price = 0;
                G41[i].count = 0;
                G32[i].num = 0;
                G32[i].price = 0;
                G32[i].count = 0;
                G42[i].avenum = 0;
                G42[i].aveprice = 0;
                G21[i].avenum = 0;
                G21[i].aveprice = 0;
                G41[i].avenum = 0;
                G41[i].aveprice = 0;
                G32[i].avenum = 0;
                G32[i].aveprice = 0;
            }
            for (int value = 0;value < data.G42_num.Count; value++)
            {
                int year = data.G42_year[value] - 2015;
                int month = data.G42_month[value];
                int index = year * 12 + month - 1;
                G42[index].num += data.G42_num[value];
                G42[index].price += data.G42_aveprice[value];
                G42[index].count += 1;
                G42[index].avenum = G42[index].num / G42[index].count;
                G42[index].aveprice = G42[index].price / G42[index].count;
                G42[index].year = year + 2015;
                G42[index].month = month;
            }
            for (int value = 0; value < data.G21_num.Count; value++)
            {
                int year = data.G21_year[value] - 2015;
                int month = data.G21_month[value];
                int index = year * 12 + month - 1;
                G21[index].num += data.G21_num[value];
                G21[index].price += data.G21_aveprice[value];
                G21[index].count += 1;
                G21[index].avenum = G21[index].num / G21[index].count;
                G21[index].aveprice = G21[index].price / G21[index].count;
                G21[index].year = year + 2015;
                G21[index].month = month;
            }
            for (int value = 0; value < data.G41_num.Count; value++)
            {
                int year = data.G41_year[value] - 2015;
                int month = data.G41_month[value];
                int index = year * 12 + month - 1;
                G41[index].num += data.G41_num[value];
                G41[index].price += data.G41_aveprice[value];
                G41[index].count += 1;
                G41[index].avenum = G41[index].num / G41[index].count;
                G41[index].aveprice = G41[index].price / G41[index].count;
                G41[index].year = year + 2015;
                G41[index].month = month;
            }
            for (int value = 0; value < data.G32_num.Count; value++)
            {
                int year = data.G32_year[value] - 2015;
                int month = data.G32_month[value];
                int index = year * 12 + month - 1;
                G32[index].num += data.G32_num[value];
                G32[index].price += data.G32_aveprice[value];
                G32[index].count += 1;
                G32[index].avenum = G32[index].num / G32[index].count;
                G32[index].aveprice = G32[index].price / G32[index].count;
                G32[index].year = year + 2015;
                G32[index].month = month;
            }
            return (G42, G21, G41, G32);
        }

        static (double, int, int, double, int, int, double) Cal_num(Data_count[] data)
        {
            double average = CalculateAverage_num(data);
            var (max, max_year, max_month) = CalculateMax_num(data);
            var (min, min_year, min_month) = CalculateMin_num(data);
            return (max, max_year, max_month, min, min_year, min_month, average);
        }

        static (double, int, int, double, int, int, double) Cal_price(Data_count[] data)
        {
            double average = CalculateAverage_price(data);
            var (max, max_year, max_month) = CalculateMax_price(data);
            var (min, min_year, min_month) = CalculateMin_price(data);
            return (max, max_year, max_month, min, min_year, min_month, average);
        }

        // 計算平均值
        static double CalculateAverage_num(Data_count[] data)
        {
            double total = 0;
            for (int i = 0;i < 120; i++)
            {
                total += data[i].avenum;
            }
            return Math.Round(total / data.Length);
        }

        // 計算最大值
        static (int, int, int) CalculateMax_num(Data_count[] data)
        {
            int max = int.MinValue;
            int year = 0, month = 0;

            for (int i = 0; i < 120; i++)
            {
                if (data[i].avenum > max)
                {
                    max = data[i].avenum;
                    year = (i / 12) + 2015;
                    month = i % 12;
                }
            }
            return (max, year, month);
        }


        // 計算最小值
        static (int, int, int) CalculateMin_num(Data_count[] data)
        {
            int min = int.MaxValue;
            int year = 0, month = 0;

            for (int i = 0; i < 120; i++)
            {
                if (data[i].avenum < min)
                {
                    min = data[i].avenum;
                    year = (i / 12) + 2015;
                    month = i % 12;
                }
            }
            return (min, year, month);
        }

        static double CalculateAverage_price(Data_count[] data)
        {
            double total = 0;
            for (int i = 0; i < 120; i++)
            {
                total += data[i].aveprice;
            }
            return Math.Round(total / data.Length);
        }

        // 計算最大值
        static (double, int, int) CalculateMax_price(Data_count[] data)
        {
            double max = double.MinValue;
            int year = 0, month = 0;

            for (int i = 0; i < 120; i++)
            {
                if (data[i].aveprice > max)
                {
                    max = data[i].aveprice;
                    year = (i / 12) + 2014;
                    month = i % 12;
                }
            }
            return (max, year, month);
        }


        // 計算最小值
        static (double, int, int) CalculateMin_price(Data_count[] data)
        {
            double min = int.MaxValue;
            int year = 0, month = 0;

            for (int i = 0; i < 120; i++)
            {
                if (data[i].aveprice < min)
                {
                    min = data[i].aveprice;
                    year = i + 57;
                    month = i % 12;
                }
            }
            return (min, year, month);
        }
    }
}