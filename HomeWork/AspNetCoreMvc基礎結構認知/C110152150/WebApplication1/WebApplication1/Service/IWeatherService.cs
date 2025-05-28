using WeatherDataMVC.Models;

namespace WeatherDataMVC.Services
{
    public interface IWeatherService
    {
        List<Station> GetAllStations();
        Station GetStationById(string id);
        void LoadFromJson(string json);
    }
}