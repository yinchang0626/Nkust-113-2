using CsvHelper;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using CameraRecord.Models;  // ✅ 正確命名空間

namespace CameraRecord.Services  // ← 這裡你也可以維持 CameraRecord 或改用你的專案名稱
{
    public static class CsvHelperService
    {
        public static List<Models.Record> LoadCameraRecords(string filePath) // 修正：加上 Models 命名空間
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<Models.Record>().ToList(); // 修正：加上 Models 命名空間
            return records;
        }
    }
}
