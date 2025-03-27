using System;
using System.Linq;
using System.Xml.Linq;
using ConsoleApp1.Models;
using ConsoleApp1.Services;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"D:\nkust\HomeWork\OpenData分析資料練習\C110152338_group\ConsoleApp1\ConsoleApp1\Data\110年高雄市消防局緊急救護服務統計表.xml";

            try
            {
                // 讀取 XML 檔案
                XDocument doc = XDocument.Load(filePath);

                // 解析資料並轉換成物件
                var rescueData = doc.Descendants("row")
                                    .Select(r => new RescueData
                                    {
                                        Month = (int)r.Element("Col1"),
                                        GeneralAmbulance = (int)r.Element("Col2"),
                                        ICUAmbulance = (int)r.Element("Col3"),
                                        Transported = (int)r.Element("Col4"),
                                        NotTransported = (int)r.Element("Col5")
                                    }).ToList();

                // 顯示成功讀取的資料筆數
                Console.WriteLine($"成功讀取 {rescueData.Count} 筆資料");

                // 顯示部分關鍵欄位的內容
                foreach (var data in rescueData)
                {
                    Console.WriteLine($"月份: {data.Month}, 一般救護車: {data.GeneralAmbulance}, 送醫次數: {data.Transported}, 未送醫次數: {data.NotTransported}");
                }

                // 進行資料分析 - 篩選送醫次數大於 10000 的資料
                var highTransportedData = rescueData.Where(r => r.Transported > 10000);
                Console.WriteLine("\n送醫次數大於 10000 的資料：");
                foreach (var data in highTransportedData)
                {
                    Console.WriteLine($"月份: {data.Month}, 送醫次數: {data.Transported}");
                }

                // 計算送醫次數的平均值
                var averageTransported = rescueData.Average(r => r.Transported);
                Console.WriteLine($"\n送醫次數平均值: {averageTransported}");

                // 計算送醫次數的最大值與最小值
                var maxTransported = rescueData.Max(r => r.Transported);
                var minTransported = rescueData.Min(r => r.Transported);
                Console.WriteLine($"\n最大送醫次數: {maxTransported}, 最小送醫次數: {minTransported}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"發生錯誤: {ex.Message}");
            }
        }
    }
}
