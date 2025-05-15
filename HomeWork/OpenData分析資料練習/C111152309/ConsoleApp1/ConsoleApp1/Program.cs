using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace DataAnalyzer
{
	// 定義資料模型
	public class Facility
	{
		public string FacilityClassification { get; set; }
		public string District { get; set; }
		public string FacilityName { get; set; }
		public string FacilityAddress { get; set; }
		public string Recent { get; set; }
		public string OpenDay { get; set; }
		public string OpenTime { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public string Remarks { get; set; }
	}

	// CSV 映射配置
	public sealed class FacilityMap : ClassMap<Facility>
	{
		public FacilityMap()
		{
			Map(m => m.FacilityClassification).Name("FacilityClassification");
			Map(m => m.District).Name("District");
			Map(m => m.FacilityName).Name("FacilityName");
			Map(m => m.FacilityAddress).Name("FacilityAddress");
			Map(m => m.Recent).Name("Recent");
			Map(m => m.OpenDay).Name("OpenDay");
			Map(m => m.OpenTime).Name("OpenTime");
			Map(m => m.Latitude).Name("latitude").TypeConverterOption.CultureInfo(CultureInfo.InvariantCulture);
			Map(m => m.Longitude).Name("longitude").TypeConverterOption.CultureInfo(CultureInfo.InvariantCulture);
			Map(m => m.Remarks).Name("Remarks");
		}
	}

	// 資料解析器介面
	public interface IDataParser<T>
	{
		IEnumerable<T> ParseData(string filePath);
	}

	// CSV 資料解析器
	public class CsvDataParser : IDataParser<Facility>
	{
		public IEnumerable<Facility> ParseData(string filePath)
		{
			using var reader = new StreamReader(filePath);
			using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
			csv.Context.RegisterClassMap<FacilityMap>();
			return csv.GetRecords<Facility>().ToList();
		}
	}

	// 資料分析器
	public class DataAnalyzer
	{
		private readonly IEnumerable<Facility> _facilities;

		public DataAnalyzer(IEnumerable<Facility> facilities)
		{
			_facilities = facilities;
		}

		public int GetTotalCount() => _facilities.Count();

		public void DisplayBasicInfo(int count = 5)
		{
			Console.WriteLine("顯示前 {0} 筆資料的關鍵欄位：", count);
			Console.WriteLine("--------------------------------------------------");

			foreach (var facility in _facilities.Take(count))
			{
				Console.WriteLine($"名稱: {facility.FacilityName}");
				Console.WriteLine($"區域: {facility.District}");
				Console.WriteLine($"地址: {facility.FacilityAddress}");
				Console.WriteLine($"開放時間: {facility.OpenTime}");
				Console.WriteLine($"緯度: {facility.Latitude}, 經度: {facility.Longitude}");
				Console.WriteLine("--------------------------------------------------");
			}
		}

		public void AnalyzeByDistrict()
		{
			var districtGroups = _facilities
				.GroupBy(f => f.District)
				.Select(g => new {
					District = g.Key,
					Count = g.Count(),
					AvgLatitude = g.Average(f => f.Latitude),
					AvgLongitude = g.Average(f => f.Longitude)
				})
				.OrderByDescending(g => g.Count);

			Console.WriteLine("\n按區域統計分析：");
			Console.WriteLine("--------------------------------------------------");
			foreach (var group in districtGroups)
			{
				Console.WriteLine($"區域: {group.District}");
				Console.WriteLine($"設施數量: {group.Count}");
				Console.WriteLine($"平均緯度: {group.AvgLatitude:F6}, 平均經度: {group.AvgLongitude:F6}");
				Console.WriteLine("--------------------------------------------------");
			}
		}

		public void FindFacilitiesByKeyword(string keyword)
		{
			var results = _facilities
				.Where(f => f.FacilityName.Contains(keyword) ||
							f.FacilityAddress.Contains(keyword) ||
							f.Remarks.Contains(keyword))
				.ToList();

			Console.WriteLine($"\n找到 {results.Count} 筆包含 '{keyword}' 的設施：");
			Console.WriteLine("--------------------------------------------------");
			foreach (var facility in results)
			{
				Console.WriteLine($"名稱: {facility.FacilityName}");
				Console.WriteLine($"地址: {facility.FacilityAddress}");
				Console.WriteLine($"備註: {facility.Remarks}");
				Console.WriteLine("--------------------------------------------------");
			}
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("資料集分析程式");
			Console.WriteLine("--------------------------------------------------");

			// 檔案路徑 - 實際使用時請替換為正確路徑
			string filePath = "C:\\Users\\buta\\Downloads\\殯葬設施(列管公墓及公墓)彙整表--1120323修.csv";

			try
			{
				// 解析資料
				IDataParser<Facility> parser = new CsvDataParser();
				var facilities = parser.ParseData(filePath);

				// 分析資料
				var analyzer = new DataAnalyzer(facilities);

				// 顯示總筆數
				Console.WriteLine($"資料總筆數: {analyzer.GetTotalCount()}");
				Console.WriteLine("--------------------------------------------------");

				// 顯示部分關鍵欄位
				analyzer.DisplayBasicInfo(3);

				// 進階分析
				analyzer.AnalyzeByDistrict();

				// 搜尋範例
				analyzer.FindFacilitiesByKeyword("公墓");

				// 更多 LINQ 查詢範例
				var topDistricts = facilities
					.GroupBy(f => f.District)
					.OrderByDescending(g => g.Count())
					.Take(3)
					.Select(g => g.Key);

				Console.WriteLine("\n設施數量最多的三個區域:");
				Console.WriteLine(string.Join(", ", topDistricts));
			}
			catch (Exception ex)
			{
				Console.WriteLine($"發生錯誤: {ex.Message}");
			}

			Console.WriteLine("\n按任意鍵結束...");
			Console.ReadKey();
		}
	}
}