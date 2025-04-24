using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using WebApp.Models;

namespace WebApp.Services
{
    public class RescueService : IRescueService
    {
        // 讀取 XML 資料
        public List<RescueData> LoadData()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory; // 取得執行檔目錄
            string projectRoot = Directory.GetParent(baseDir).Parent.Parent.Parent.FullName; // 回到專案根目錄
            string filePath = Path.Combine(projectRoot, "Data", "110年高雄市消防局緊急救護服務統計表.xml");

            try
            {
                XDocument doc = XDocument.Load(filePath);

                var rescueData = doc.Descendants("row")
                    .Select(r => new RescueData
                    {
                        Month = (int)r.Element("Col1"),
                        GeneralAmbulance = (int)r.Element("Col2"),
                        ICUAmbulance = (int)r.Element("Col3"),
                        Transported = (int)r.Element("Col4"),
                        NotTransported = (int)r.Element("Col5")
                    }).ToList();

                return rescueData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"發生錯誤: {ex.Message}");
                return new List<RescueData>(); // 若發生錯誤，返回空列表
            }
        }
    }
}
