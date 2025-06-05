using CemeteryWeb.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Formats.Asn1;
using System.Globalization;

namespace CemeteryWeb.Services
{
    // Services/CemeteryService.cs
    public class CemeteryService : ICemeteryService
    {
        private readonly List<Cemetery> _cemeteries = new();

        public async Task<List<Cemetery>> GetAllCemeteriesAsync()
        {
            return await Task.FromResult(_cemeteries);
        }

        public async Task<Cemetery> GetCemeteryByNameAsync(string name)
        {
            return await Task.FromResult(_cemeteries.FirstOrDefault(c => c.FacilityName == name));
        }

        public async Task UploadCemeteryDataAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty");

            using var reader = new StreamReader(file.OpenReadStream());
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
                HeaderValidated = null // 忽略標頭驗證
            });

            csv.Context.RegisterClassMap<CemeteryMap>(); // 註冊映射

            _cemeteries.Clear();
            _cemeteries.AddRange(csv.GetRecords<Cemetery>().ToList());
        }

        public async Task<List<Cemetery>> GenerateReportAsync(string district)
        {
            return await Task.FromResult(_cemeteries
                .Where(c => string.IsNullOrEmpty(district) || c.District == district)
                .ToList());
        }
        public int GetCount()
        {
            return _cemeteries.Count;
        }
    }
}
