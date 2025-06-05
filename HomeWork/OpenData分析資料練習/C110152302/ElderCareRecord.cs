using CsvHelper.Configuration.Attributes;

public class ElderCareRecord
{
    [Name("行政區")]
    public string? Area { get; set; }

    [Name("人數")]
    public int People { get; set; }

    [Name("電話問安")]
    public string? PhoneGreetingsRaw { get; set; }

    [Name("關懷訪視")]
    public string? VisitsRaw { get; set; }

    [Name("就醫協助")]
    public int MedicalHelp { get; set; }

    [Name("生活協助")]
    public int LifeHelp { get; set; }

    [Name("期底安裝緊急救援連線人數（人）")]
    public int Emergency { get; set; }

    [Name("長照服務")]
    public int LongTermCare { get; set; }

    [Name("服務合計")]
    public string? TotalServiceRaw { get; set; }

    public int PhoneGreetings => int.Parse(PhoneGreetingsRaw.Replace(",", ""));
    public int Visits => int.Parse(VisitsRaw.Replace(",", ""));
    public int TotalService => int.Parse(TotalServiceRaw.Replace(",", ""));
}
