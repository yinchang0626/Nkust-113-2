using CemeteryWeb.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CemeteryWeb.Services
{
    public interface IRecheckCaseService
    {
        Task<List<RecheckCase>> GetAllCasesAsync();
        Task<RecheckCase?> GetCaseAsync(int year, int month, string hospital);
        Task UploadCasesAsync(IFormFile file);
    }
}