namespace CemeteryWeb.Services
{
    // 在 Services 文件夾中創建 CemeteryMap.cs
    using CsvHelper.Configuration;

    public sealed class CemeteryMap : ClassMap<Models.Cemetery>
    {
        public CemeteryMap()
        {
            Map(m => m.FacilityClassification).Name("FacilityClassification");
            Map(m => m.District).Name("District");
            Map(m => m.FacilityName).Name("FacilityName");
            Map(m => m.FacilityAddress).Name("FacilityAddress");
            Map(m => m.Recent).Name("Recent");
            Map(m => m.OpenDay).Name("OpenDay");
            Map(m => m.OpenTime).Name("OpenTime");
            Map(m => m.Latitude).Name("latitude");  // 注意小寫
            Map(m => m.Longitude).Name("longitude"); // 注意小寫
            Map(m => m.Remarks).Name("Remarks");
        }
    }
}
