namespace CemeteryWeb.Models
{
    // 役男複檢案件統計資料模型
    public class RecheckCase
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string? Hospital { get; set; }
        public int PublicCount { get; set; }
        public int PrivateCount { get; set; }
    }
}