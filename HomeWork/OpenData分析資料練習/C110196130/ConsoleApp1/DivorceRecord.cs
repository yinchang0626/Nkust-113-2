// 定義 DivorceRecord 類別
using CsvHelper.Configuration;

public class DivorceRecord
{
    public string 統計期 { get; set; }
    public string 登記發生日期 { get; set; }
    public int 離婚對數總計 { get; set; }
    public int 離婚對數相同性別 { get; set; }
    public int 離婚對數本國國籍配偶 { get; set; }
    public int 離婚對數大陸港澳配偶 { get; set; }
    public int 離婚對數其他外籍配偶 { get; set; }
    public double 離婚對數外籍配偶比例 { get; set; }
    public double 平均離婚年齡男 { get; set; }
    public double 平均離婚年齡女 { get; set; }
}

// 定義 DivorceRecord 類別的 ClassMap
public class DivorceRecordMap : ClassMap<DivorceRecord>
{
    public DivorceRecordMap()
    {
        Map(m => m.統計期).Name("統計期");
        Map(m => m.登記發生日期).Name("登記發生日期");
        Map(m => m.離婚對數總計).Name("離婚對數總計");
        Map(m => m.離婚對數相同性別).Name("離婚對數相同性別");
        Map(m => m.離婚對數本國國籍配偶).Name("離婚對數對象為本國國籍配偶");
        Map(m => m.離婚對數大陸港澳配偶).Name("離婚對數對象為大陸港澳配偶");
        Map(m => m.離婚對數其他外籍配偶).Name("離婚對數對象為其他外籍配偶");
        Map(m => m.離婚對數外籍配偶比例).Name("離婚對數中配偶為大陸港澳及其他外籍人士之對數比率");
        Map(m => m.平均離婚年齡男).Name("平均離婚年齡男");
        Map(m => m.平均離婚年齡女).Name("平均離婚年齡女");
    }
}