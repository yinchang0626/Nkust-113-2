using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using C110196130.Models;

namespace C110196130.Services  // 這一行要加上
{
    public class CsvService
    {
        public IEnumerable<DivorceRecord> ReadCsvFile(Stream fileStream)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ",",
                HeaderValidated = null
            };

            using var reader = new StreamReader(fileStream);
            using var csv = new CsvReader(reader, config);
            csv.Context.RegisterClassMap<DivorceRecordMap>(); // 註冊映射類別
            return csv.GetRecords<DivorceRecord>().ToList();
        }

        public IEnumerable<DivorceRecord> ReadCsvFile(string filePath)
        {
            using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return ReadCsvFile(stream);
        }
    }
}
