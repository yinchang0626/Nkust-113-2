using CemeteryWeb.Models;

namespace CemeteryWeb.Services
{
    // Services/ICemeteryService.cs
    public interface ICemeteryService
    {
        Task<List<Cemetery>> GetAllCemeteriesAsync();
        Task<Cemetery> GetCemeteryByNameAsync(string name);
        Task UploadCemeteryDataAsync(IFormFile file);
        Task<List<Cemetery>> GenerateReportAsync(string district);
        int GetCount();
    }
}
