using CemeteryWeb.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CemeteryWeb.Services
{
    public interface ICareServiceService
    {
        Task<List<CareService>> GetAllCareServicesAsync();
        Task<CareService?> GetCareServiceByNameAsync(string name);
        Task UploadCareServiceDataAsync(IFormFile file);
    }
}
