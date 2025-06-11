namespace CemeteryWeb.Models
{
    // Models/Cemetery.cs
    public class Cemetery
    {
        public string FacilityClassification { get; set; }
        public string District { get; set; }
        public string FacilityName { get; set; }
        public string FacilityAddress { get; set; }
        public string Recent { get; set; }
        public string OpenDay { get; set; }
        public string OpenTime { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Remarks { get; set; }
    }
}
