using System.Text.Json.Serialization;

public class AirQualityRecord
{
    public string? 項目 { get; set; }
    public string? 值 { get; set; }
    public string? 測站 { get; set; }
    public string? 環境空氣品質標準 { get; set; }

    [JsonPropertyName("96年")] public string? _96年 { get; set; }
    [JsonPropertyName("97年")] public string? _97年 { get; set; }
    [JsonPropertyName("98年")] public string? _98年 { get; set; }
    [JsonPropertyName("99年")] public string? _99年 { get; set; }
    [JsonPropertyName("100年")] public string? _100年 { get; set; }
    [JsonPropertyName("101年")] public string? _101年 { get; set; }
    [JsonPropertyName("102年")] public string? _102年 { get; set; }
    [JsonPropertyName("103年")] public string? _103年 { get; set; }
    [JsonPropertyName("104年")] public string? _104年 { get; set; }
    [JsonPropertyName("105年")] public string? _105年 { get; set; }
}
