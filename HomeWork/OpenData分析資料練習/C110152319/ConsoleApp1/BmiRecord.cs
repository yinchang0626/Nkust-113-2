// CSV 列與 BmiRecord 類屬性之間的映射
using CsvHelper.Configuration;

public class BmiRecordMap : ClassMap<BmiRecord>
{
    public BmiRecordMap()
    {
        Map(m => m.年).Name("年");
        Map(m => m.月).Name("月");
        Map(m => m.BMI值體位).Name("BMI值體位");
        Map(m => m.體位).Name("體位");
        Map(m => m.人數).Name("人數");
        Map(m => m.百分比).Name("百分比");
    }
}

public class BmiRecord
{
    public string 年 { get; set; } // 年
    public string 月 { get; set; } // 月
    public string BMI值體位 { get; set; } // BMI值體位
    public string 體位 { get; set; } // 體位
    public int 人數 { get; set; } // 人數
    public string 百分比 { get; set; } // 百分比
}