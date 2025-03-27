using System;
using System.Diagnostics;
using System.IO;
using ConsoleApp1.Services;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var rescueService = new RescueService();

            // 讀取資料
            var rescueData = rescueService.LoadData();
            if (rescueData.Count > 0)
            {
                // 顯示成功讀取的資料筆數
                Console.WriteLine($"成功讀取 {rescueData.Count} 筆資料");

                // 輸出資料至 HTML 檔案
                rescueService.ExportToHtml(rescueData);

                // 開啟 HTML 文件
                OpenHtmlReport();
            }
            else
            {
                Console.WriteLine("未讀取到資料");
            }
        }

        // 打開 HTML 報告的方法
        static void OpenHtmlReport()
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory; // 取得執行檔目錄
                string projectRoot = Directory.GetParent(baseDir).Parent.Parent.Parent.FullName; // 回到 ConsoleApp1 根目錄
                string htmlFolderPath = Path.Combine(projectRoot, "HTML");
                string htmlFilePath = Path.Combine(htmlFolderPath, "RescueDataReport.html");

                // 檢查 HTML 檔案是否存在
                if (File.Exists(htmlFilePath))
                {
                    // 啟動默認的瀏覽器開啟 HTML 檔案
                    Process.Start(new ProcessStartInfo(htmlFilePath) { UseShellExecute = true });
                    Console.WriteLine("報告已開啟於默認瀏覽器");
                }
                else
                {
                    Console.WriteLine("未找到 HTML 報告文件");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"開啟 HTML 報告時發生錯誤: {ex.Message}");
            }
        }
    }
}
