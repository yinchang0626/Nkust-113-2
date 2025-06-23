using System;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ScottPlot;

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
            var filePath = @"D:/sister hw/1101/1011/1011/2023.csv";
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
            var visionStatusPattern = new Regex(@"(視力不良|視力檢查|裸視視力檢查)");

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
                .GroupBy(r => new
                {
                    Grade = gradePattern.Match(r.欄位名稱).Success ? gradePattern.Match(r.欄位名稱).Value : "未分類",
                    Gender = genderPattern.Match(r.欄位名稱).Success ? genderPattern.Match(r.欄位名稱).Groups[1].Value : "未分類"
                })
                .Select(g =>
                {
                    var gradeGender = g.Key;
                    var totalStudents = g.Where(r => r.欄位名稱.Contains("檢查")).Sum(r => r.數值);
                    var nearsightedStudents = g.Where(r => r.欄位名稱.Contains("不良")).Sum(r => r.數值);
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
                .Where(x => x.Grade != "未分類" && x.Gender != "未分類")
                .ToList();

            Console.WriteLine("\n各年級男女生總人數、近視人數及近視比例:");
            foreach (var stat in gradeGenderStats)
            {
                Console.WriteLine($"{stat.Grade} {stat.Gender}: 總人數 = {stat.TotalStudents}, 近視人數 = {stat.NearsightedStudents}, 近視比例 = {stat.NearsightedRatio:F2}%");
            }

            var totalStudentsOverall = records.Where(r => r.欄位名稱.Contains("檢查")).Sum(r => r.數值);
            var totalNearsightedOverall = records.Where(r => r.欄位名稱.Contains("不良")).Sum(r => r.數值);
            var overallNearsightedRatio = totalStudentsOverall > 0 ? (double)totalNearsightedOverall / totalStudentsOverall * 100 : 0;

            var maleTotal = records.Where(r => r.欄位名稱.Contains("男") && r.欄位名稱.Contains("檢查")).Sum(r => r.數值);
            var maleNearsighted = records.Where(r => r.欄位名稱.Contains("男") && r.欄位名稱.Contains("不良")).Sum(r => r.數值);
            var femaleTotal = records.Where(r => r.欄位名稱.Contains("女") && r.欄位名稱.Contains("檢查")).Sum(r => r.數值);
            var femaleNearsighted = records.Where(r => r.欄位名稱.Contains("女") && r.欄位名稱.Contains("不良")).Sum(r => r.數值);

            var maleNearsightedRatio = maleTotal > 0 ? (double)maleNearsighted / maleTotal * 100 : 0;
            var femaleNearsightedRatio = femaleTotal > 0 ? (double)femaleNearsighted / femaleTotal * 100 : 0;

            Console.WriteLine($"\n所有學生的近視比例: {overallNearsightedRatio:F2}%");
            Console.WriteLine($"所有男生的近視比例: {maleNearsightedRatio:F2}%");
            Console.WriteLine($"所有女生的近視比例: {femaleNearsightedRatio:F2}%");

            // ✅ 圖表 1：群組柱狀圖
            var grades = gradeGenderStats.Select(g => g.Grade).Distinct().OrderBy(g => g).ToList();
            var maleRatios = grades.Select(g => gradeGenderStats.FirstOrDefault(x => x.Grade == g && x.Gender == "男")?.NearsightedRatio ?? 0).ToArray();
            var femaleRatios = grades.Select(g => gradeGenderStats.FirstOrDefault(x => x.Grade == g && x.Gender == "女")?.NearsightedRatio ?? 0).ToArray();

            var barPlot = new Plot(800, 500);
            double[][] values = new double[][] { maleRatios, femaleRatios };
            string[] seriesLabels = { "男生", "女生" };
            string[] gradeLabels = grades.ToArray();

            // 建立空的誤差值（全為 0）
            double[][] yErr = new double[][]
            {
                new double[maleRatios.Length], // 全為 0
                new double[femaleRatios.Length]
            };

            barPlot.AddBarGroups(gradeLabels, seriesLabels, values, yErr);
            barPlot.Title("各年級男女近視比例");
            barPlot.YLabel("近視比例 (%)");
            barPlot.SetAxisLimits(yMin: 0);
            barPlot.Legend();
            barPlot.SaveFig("grade_gender_ratio.png");
            //Console.WriteLine("✅ 輸出圖：grade_gender_ratio.png");


            // ✅ 圖表 2：圓餅圖
            var piePlot = new Plot(400, 400);
            var pie = piePlot.AddPie(new double[] { maleNearsightedRatio, femaleNearsightedRatio });
            pie.SliceLabels = new[] { "男生", "女生" };
            pie.ShowPercentages = true;
            pie.ShowValues = false; // ❌ 不顯示實際數值
            pie.DonutSize = 0.5;
            pie.Explode = true;

            // ✅ 加入圖例（顯示在右下角）
            piePlot.Legend(location: Alignment.LowerRight);

            piePlot.Title("男女整體近視比例");
            piePlot.SaveFig("gender_pie.png");
            //Console.WriteLine("✅ 輸出圖：gender_pie.png");


        }
    }
}
