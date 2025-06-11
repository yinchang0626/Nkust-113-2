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
    public class RecheckCaseService : IRecheckCaseService
    {
        private static List<RecheckCase> _cases = new();

        public async Task<List<RecheckCase>> GetAllCasesAsync()
        {
            return await Task.FromResult(_cases);
        }

        public async Task<RecheckCase?> GetCaseAsync(int year, int month, string hospital)
        {
            return await Task.FromResult(_cases.FirstOrDefault(c => c.Year == year && c.Month == month && c.Hospital == hospital));
        }

        public async Task UploadCasesAsync(IFormFile file)
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
            csv.Context.RegisterClassMap<RecheckCaseMap>();
            var newCases = csv.GetRecords<RecheckCase>().ToList();
            _cases = newCases;
        }
    }
}