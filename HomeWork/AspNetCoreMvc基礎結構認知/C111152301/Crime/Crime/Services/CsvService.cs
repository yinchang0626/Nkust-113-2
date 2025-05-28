using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Crime.Models;
using Crime.Data;
using CsvHelper.Configuration.Attributes;
public class CsvService
{
    private readonly ApplicationDbContext _context;

    public CsvService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> ImportCsvAsync(Stream fileStream)
    {
        using var reader = new StreamReader(fileStream);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound = null
        });

        var records = csv.GetRecords<CrimeCsvRaw>().ToList();

        var converted = records.Select(r => new CrimeStat
        {
            Year = int.TryParse(r.統計期.Split('年')[0], out var y) ? y + 1911 : 0,
            Month = int.TryParse(r.統計期.Split('年')[1].Replace("月", "").Trim(), out var m) ? m : 1,
            TotalCases = int.TryParse(r.發生總計, out var total) ? total : 0,
            RapeCases = int.TryParse(r.強制性交, out var rape) ? rape : 0,
            RobberyCases = int.TryParse(r.強盜搶奪, out var rob) ? rob : 0,
            OtherCases = int.TryParse(r.其他, out var other) ? other : 0,
            IncidentRate = double.TryParse(r.發生率, out var ir) ? ir : 0,
            Suspects = int.TryParse(r.嫌疑犯, out var sus) ? sus : 0,
            CrimeRate = double.TryParse(r.犯罪率, out var cr) ? cr : 0,
            SolvedTotal = int.TryParse(r.破獲總計, out var st) ? st : 0,
            SolvedCurrent = int.TryParse(r.破獲當期, out var sc) ? sc : 0,
            SolvedBacklog = int.TryParse(r.破獲積案, out var sb) ? sb : 0,
            SolvedOther = int.TryParse(r.破獲他轄, out var so) ? so : 0,
            SolveRate = double.TryParse(r.破獲率, out var sr) ? sr : 0
        }).ToList();

        _context.CrimeStats.AddRange(converted);
        await _context.SaveChangesAsync();
        return converted.Count;
    }

    public class CrimeCsvRaw
    {
        [Name("統計期")]
        public string 統計期 { get; set; }

        [Name("發生件數/總計[件]")]
        public string 發生總計 { get; set; }

        [Name("發生件數/強制性交[件]")]
        public string 強制性交 { get; set; }

        [Name("發生件數/強盜搶奪[件]")]
        public string 強盜搶奪 { get; set; }

        [Name("發生件數/其他[件]")]
        public string 其他 { get; set; }

        [Name("發生率[件/十萬人]")]
        public string 發生率 { get; set; }

        [Name("嫌疑犯[人]")]
        public string 嫌疑犯 { get; set; }

        [Name("犯罪人口率[人/十萬人]")]
        public string 犯罪率 { get; set; }

        [Name("破獲件數/總計[件]")]
        public string 破獲總計 { get; set; }

        [Name("破獲件數/當期[件]")]
        public string 破獲當期 { get; set; }

        [Name("破獲件數/積案[件]")]
        public string 破獲積案 { get; set; }

        [Name("破獲件數/他轄[件]")]
        public string 破獲他轄 { get; set; }

        [Name("破獲率[%]")]
        public string 破獲率 { get; set; }
    }
}