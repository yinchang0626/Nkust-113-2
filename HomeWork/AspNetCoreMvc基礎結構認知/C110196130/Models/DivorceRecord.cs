namespace C110196130.Models
{
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

}
