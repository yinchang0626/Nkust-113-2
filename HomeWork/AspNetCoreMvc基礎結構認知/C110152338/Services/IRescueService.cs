using WebApp.Models;

namespace WebApp.Services
{
    public interface IRescueService
    {
        List<RescueData> LoadData();
    }
}