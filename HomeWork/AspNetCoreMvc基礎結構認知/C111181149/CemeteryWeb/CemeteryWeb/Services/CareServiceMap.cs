using CsvHelper.Configuration;

namespace CemeteryWeb.Services
{
    // 對應 CareService 的 CsvHelper 映射
    public sealed class CareServiceMap : ClassMap<Models.CareService>
    {
        public CareServiceMap()
        {
            Map(m => m.FacilityName).Name("FacilityName");
            Map(m => m.ServiceArea).Name("ServiceArea");
            Map(m => m.ContactPhone).Name("ContactPhone");
        }
    }
}