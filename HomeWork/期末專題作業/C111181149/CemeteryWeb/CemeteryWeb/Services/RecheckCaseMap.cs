using CsvHelper.Configuration;

namespace CemeteryWeb.Services
{
    // 對應 RecheckCase 的 CsvHelper 映射
    public sealed class RecheckCaseMap : ClassMap<Models.RecheckCase>
    {
        public RecheckCaseMap()
        {
            Map(m => m.Year).Name("年");
            Map(m => m.Month).Name("月");
            Map(m => m.Hospital).Name("複檢醫院");
            Map(m => m.PublicCount).Name("公費人數");
            Map(m => m.PrivateCount).Name("自費人數");
        }
    }
}