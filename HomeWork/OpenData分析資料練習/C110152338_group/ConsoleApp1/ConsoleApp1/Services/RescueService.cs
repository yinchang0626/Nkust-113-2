using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ConsoleApp1.Models;

namespace ConsoleApp1.Services
{
    public class RescueService
    {
        // 讀取 XML 資料
        public List<RescueData> LoadData()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory; // 取得執行檔目錄
            string projectRoot = Directory.GetParent(baseDir).Parent.Parent.Parent.FullName; // 回到 ConsoleApp1 根目錄
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
        // 讀取資料，並進行簡單的資料篩選、計算統計
        public void ExportToHtml(List<RescueData> rescueData)
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory; // 取得執行檔目錄
                string projectRoot = Directory.GetParent(baseDir).Parent.Parent.Parent.FullName; // 回到 ConsoleApp1 根目錄
                string htmlFolderPath = Path.Combine(projectRoot, "HTML");

                if (!Directory.Exists(htmlFolderPath))
                {
                    Directory.CreateDirectory(htmlFolderPath); // 如果 HTML 資料夾不存在，則創建
                }

                string htmlFilePath = Path.Combine(htmlFolderPath, "RescueDataReport.html");

                // 開始構建 HTML 內容
                StringBuilder htmlContent = new StringBuilder();
                htmlContent.Append("<html><head><title>救護車資料報告</title>");
                htmlContent.Append("<script src='https://cdn.jsdelivr.net/npm/chart.js'></script>");
                htmlContent.Append("<style>");
                htmlContent.Append("body { font-family: Arial, sans-serif; background-color: #f4f4f9; color: #333; margin: 0; padding: 0; text-align: center; }");
                htmlContent.Append("h1 { margin-top: 20px; color: #5e5e5e; }");
                htmlContent.Append("h2 { color: #5e5e5e; font-size: 24px; margin-bottom: 10px; }");
                htmlContent.Append("table { width: 80%; margin: 20px auto; border-collapse: collapse; text-align: center; }");
                htmlContent.Append("th, td { padding: 10px; border: 1px solid #ddd; }");
                htmlContent.Append("th { background-color: #4CAF50; color: white; }");
                htmlContent.Append("tr:nth-child(even) { background-color: #f2f2f2; }");
                htmlContent.Append("p { font-size: 18px; color: #555; }");
                htmlContent.Append("canvas { display: block; margin: 20px auto; }");
                htmlContent.Append("</style>");
                htmlContent.Append("</head><body>");
                htmlContent.Append("<h1>救護車服務統計資料</h1>");

                // 計算統計資料：平均值、最大值、最小值
                var averageTransported = rescueData.Average(r => r.Transported);
                var maxTransported = rescueData.Max(r => r.Transported);
                var minTransported = rescueData.Min(r => r.Transported);

                // 顯示統計資訊
                htmlContent.Append("<h2>統計資訊</h2>");
                htmlContent.Append($"<p>送醫次數平均值: {averageTransported}</p>");
                htmlContent.Append($"<p>送醫次數最大值: {maxTransported}</p>");
                htmlContent.Append($"<p>送醫次數最小值: {minTransported}</p>");

                // 篩選送醫次數大於 10000 的資料
                var highTransportedData = rescueData.Where(r => r.Transported > 10000).ToList();

                // 顯示篩選後的資料
                htmlContent.Append("<h2>送醫次數大於 10000 的資料</h2>");
                htmlContent.Append("<table><tr><th>月份</th><th>送醫次數</th></tr>");
                foreach (var data in highTransportedData)
                {
                    htmlContent.Append($"<tr><td>{data.Month}</td><td>{data.Transported}</td></tr>");
                }
                htmlContent.Append("</table>");

                // 生成圖表
                htmlContent.Append("<h2>送醫次數圖表</h2>");
                htmlContent.Append("<canvas id='transportedChart' width='600' height='400'></canvas>");
                htmlContent.Append("<script>");
                htmlContent.Append("var ctx = document.getElementById('transportedChart').getContext('2d');");
                htmlContent.Append("var transportedChart = new Chart(ctx, {");
                htmlContent.Append("    type: 'bar',");
                htmlContent.Append("    data: {");
                htmlContent.Append("        labels: [" + string.Join(",", rescueData.Select(r => $"'{r.Month}'")) + "],");
                htmlContent.Append("        datasets: [{");
                htmlContent.Append("            label: '送醫次數',");
                htmlContent.Append("            data: [" + string.Join(",", rescueData.Select(r => r.Transported)) + "],");
                htmlContent.Append("            backgroundColor: 'rgba(54, 162, 235, 0.2)',");
                htmlContent.Append("            borderColor: 'rgba(54, 162, 235, 1)',");
                htmlContent.Append("            borderWidth: 1");
                htmlContent.Append("        }]"); // 這裡可以根據需求添加更多的資料集
                htmlContent.Append("    },");
                htmlContent.Append("    options: {");
                htmlContent.Append("        responsive: true,");
                htmlContent.Append("        plugins: {");
                htmlContent.Append("            legend: {");
                htmlContent.Append("                position: 'top',");
                htmlContent.Append("            }");
                htmlContent.Append("        },");
                htmlContent.Append("        scales: {");
                htmlContent.Append("            y: {");
                htmlContent.Append("                beginAtZero: true,");
                htmlContent.Append("                ticks: {");
                htmlContent.Append("                    stepSize: 5000");
                htmlContent.Append("                }");
                htmlContent.Append("            }");
                htmlContent.Append("        }");
                htmlContent.Append("    }");
                htmlContent.Append("});");
                htmlContent.Append("</script>");

                htmlContent.Append("</body></html>");

                // 寫入 HTML 檔案
                File.WriteAllText(htmlFilePath, htmlContent.ToString());

                Console.WriteLine($"HTML 報告已儲存到 {htmlFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"發生錯誤: {ex.Message}");
            }
        }


    }
}
