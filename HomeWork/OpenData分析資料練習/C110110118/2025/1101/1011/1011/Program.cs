using System;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ScottPlot;  // 引入 ScottPlot

namespace VisionDataAnalysis
{
    public class VisionData
    {
        public string 地區 { get; set; }
        public string 項目 { get; set; }
        public string 欄位名稱 { get; set; }
        public int 數值 { get; set; }
        public string 資料時間日期 { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var filePath = @"D:/1101/1011/1011/2023.csv"; // 新的檔案路徑
            var records = new List<VisionData>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Encoding = System.Text.Encoding.UTF8,
                HeaderValidated = null,
                MissingFieldFound = null
            };

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                records = csv.GetRecords<VisionData>().ToList();
            }

            Console.WriteLine($"2023臺中市國民中學學生裸眼視力檢查");

            var gradePattern = new Regex(@"國中_(.+?)年級");
            var schoolTypePattern = new Regex(@"^(國立|市立|私立)");
            var genderPattern = new Regex(@"_(男|女)");
            var visionStatusPattern = new Regex(@"(視力不良|視力檢查)");

            var schoolTypeCounts = records
                .GroupBy(r => schoolTypePattern.Match(r.欄位名稱).Value)
                .Select(g => new { SchoolType = g.Key, Count = g.Sum(r => r.數值) })
                .ToList();

            Console.WriteLine("\n國立/市立/私立學校之學生數量:");
            foreach (var schoolType in schoolTypeCounts)
            {
                Console.WriteLine($"{schoolType.SchoolType}: {schoolType.Count}");
            }

            var gradeGenderStats = records
                .Where(r => visionStatusPattern.IsMatch(r.欄位名稱))
                .GroupBy(r => new { Grade = gradePattern.Match(r.欄位名稱).Value, Gender = genderPattern.Match(r.欄位名稱).Value })
                .Select(g =>
                {
                    var gradeGender = g.Key;
                    var totalStudents = g.Where(r => r.欄位名稱.Contains("視力檢查")).Sum(r => r.數值);
                    var nearsightedStudents = g.Where(r => r.欄位名稱.Contains("視力不良")).Sum(r => r.數值);
                    var nearsightedRatio = totalStudents > 0 ? (double)nearsightedStudents / totalStudents * 100 : 0;

                    return new
                    {
                        Grade = gradeGender.Grade,
                        Gender = gradeGender.Gender,
                        TotalStudents = totalStudents,
                        NearsightedStudents = nearsightedStudents,
                        NearsightedRatio = nearsightedRatio
                    };
                })
                .ToList();

            Console.WriteLine("\n各年級男女生總人數、近視人數及近視比例:");
            foreach (var stat in gradeGenderStats)
            {
                Console.WriteLine($"{stat.Grade} {stat.Gender}: 總人數 = {stat.TotalStudents}, 近視人數 = {stat.NearsightedStudents}, 近視比例 = {stat.NearsightedRatio:F2}%");
            }

            var visionStatusCounts = records
                .GroupBy(r => visionStatusPattern.Match(r.欄位名稱).Value)
                .Select(g => new { VisionStatus = g.Key == "視力檢查" ? "視力檢查總人數" : g.Key, Count = g.Sum(r => r.數值) })
                .ToList();

            Console.WriteLine("\n視力狀況分類:");
            foreach (var status in visionStatusCounts)
            {
                Console.WriteLine($"{status.VisionStatus}: {status.Count}");
            }

            var totalStudentsOverall = records.Where(r => r.欄位名稱.Contains("視力檢查")).Sum(r => r.數值);
            var totalNearsightedOverall = records.Where(r => r.欄位名稱.Contains("視力不良")).Sum(r => r.數值);
            var overallNearsightedRatio = totalStudentsOverall > 0 ? (double)totalNearsightedOverall / totalStudentsOverall * 100 : 0;

            Console.WriteLine($"\n所有學生的近視比例: {overallNearsightedRatio:F2}%");

            var maleTotal = records.Where(r => r.欄位名稱.Contains("男") && r.欄位名稱.Contains("視力檢查")).Sum(r => r.數值);
            var maleNearsighted = records.Where(r => r.欄位名稱.Contains("男") && r.欄位名稱.Contains("視力不良")).Sum(r => r.數值);
            var femaleTotal = records.Where(r => r.欄位名稱.Contains("女") && r.欄位名稱.Contains("視力檢查")).Sum(r => r.數值);
            var femaleNearsighted = records.Where(r => r.欄位名稱.Contains("女") && r.欄位名稱.Contains("視力不良")).Sum(r => r.數值);

            var maleNearsightedRatio = maleTotal > 0 ? (double)maleNearsighted / maleTotal * 100 : 0;
            var femaleNearsightedRatio = femaleTotal > 0 ? (double)femaleNearsighted / femaleTotal * 100 : 0;

            Console.WriteLine($"\n所有男生的近視比例: {maleNearsightedRatio:F2}%");
            Console.WriteLine($"所有女生的近視比例: {femaleNearsightedRatio:F2}%");

            // ✅ 圖表 1：各年級男女近視比例 - 群組柱狀圖
            var grades = gradeGenderStats.Select(g => g.Grade).Distinct().OrderBy(g => g).ToList();
            var maleRatios = grades.Select(g => gradeGenderStats.FirstOrDefault(x => x.Grade == g && x.Gender == "男")?.NearsightedRatio ?? 0).ToArray();
            var femaleRatios = grades.Select(g => gradeGenderStats.FirstOrDefault(x => x.Grade == g && x.Gender == "女")?.NearsightedRatio ?? 0).ToArray();

            var barPlot = new ScottPlot.Plot(600, 400);
            var bar1 = barPlot.AddBar(maleRatios);
            bar1.PositionOffset = -0.2;
            bar1.Label = "男生";

            var bar2 = barPlot.AddBar(femaleRatios);
            bar2.PositionOffset = 0.2;
            bar2.Label = "女生";

            barPlot.XTicks(Enumerable.Range(0, grades.Count).Select(i => (double)i).ToArray(), grades.ToArray());
            barPlot.Title("各年級男女近視比例");
            barPlot.YLabel("近視比例 (%)");
            barPlot.Legend();
            barPlot.SetAxisLimits(yMin: 0);
            barPlot.SaveFig("grade_gender_ratio.png");
            Console.WriteLine("\n✅ 輸出圖：grade_gender_ratio.png");

            // ✅ 圖表 2：男女整體近視比例 - 圓餅圖
            var piePlot = new ScottPlot.Plot(400, 400);
            double[] values = { maleNearsightedRatio, femaleNearsightedRatio };
            string[] labels = { "男生", "女生" };
            piePlot.AddPie(values, labels);
            piePlot.Title("男女整體近視比例");
            piePlot.SaveFig("gender_pie.png");
            Console.WriteLine("✅ 輸出圖：gender_pie.png");
        }
    }
}
