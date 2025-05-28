using Newtonsoft.Json;
using WeatherDataMVC.Models;

namespace WeatherDataMVC.Services
{
    public class WeatherService : IWeatherService
    {
        private List<Station> _stations = new();

        public List<Station> GetAllStations() => _stations;

        public Station GetStationById(string id) => _stations.FirstOrDefault(s => s.StationId == id);

        public void LoadFromJson(string json)
        {
            var root = JsonConvert.DeserializeObject<CwaopendataRoot>(json);
            _stations = root?.cwaopendata?.dataset?.Station ?? new List<Station>();
        }
    }
}
