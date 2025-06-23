using CemeteryWeb.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CemeteryWeb.Services
{
    public class CareServiceService : ICareServiceService
    {
        private readonly List<CareService> _careServices = new();

        public async Task<List<CareService>> GetAllCareServicesAsync()
        {
            return await Task.FromResult(_careServices);
        }

        public async Task<CareService?> GetCareServiceByNameAsync(string name)
        {
            return await Task.FromResult(_careServices.FirstOrDefault(c => c.FacilityName == name));
        }

        public async Task UploadCareServiceDataAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new System.ArgumentException("File is empty");

            using var reader = new StreamReader(file.OpenReadStream());
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
                HeaderValidated = null
            });
            csv.Context.RegisterClassMap<CareServiceMap>();
            _careServices.Clear();
            _careServices.AddRange(csv.GetRecords<CareService>().ToList());
        }
    }
}
