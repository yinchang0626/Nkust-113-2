namespace WeatherDataMVC.Models
{
    public class Station
    {
        public string StationName { get; set; }
        public string StationId { get; set; }
        public ObsTime ObsTime { get; set; }
        public GeoInfo GeoInfo { get; set; }
        public WeatherElement WeatherElement { get; set; }
    }

    public class ObsTime
    {
        public string DateTime { get; set; }
    }

    public class GeoInfo
    {
        public List<Coordinate> Coordinates { get; set; }
        public string StationAltitude { get; set; }
    }

    public class Coordinate
    {
        public string CoordinateName { get; set; }
        public string CoordinateFormat { get; set; }
        public string StationLatitude { get; set; }
        public string StationLongitude { get; set; }
    }

    public class WeatherElement
    {
        public string SolarRadiation { get; set; }
    }

    public class CwaopendataRoot
    {
        public Cwaopendata cwaopendata { get; set; }
    }

    public class Cwaopendata
    {
        public Dataset dataset { get; set; }
    }

    public class Dataset
    {
        public List<Station> Station { get; set; }
    }
}