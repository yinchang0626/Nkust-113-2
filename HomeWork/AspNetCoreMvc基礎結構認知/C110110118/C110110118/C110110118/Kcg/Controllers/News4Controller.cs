using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Kcg.Models;
using System.IO;

namespace Kcg.Controllers
{
    public class News4Controller : Controller
    {
        private List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product { Id = 1, ProductName = "Apple", UnitPrice = 30.5m, Quantity = 4 },
                new Product { Id = 2, ProductName = "Banana", UnitPrice = 15.0m, Quantity = 5 },
                new Product { Id = 3, ProductName = "Orange", UnitPrice = 20.0m, Quantity = 5 }
            };
        }

        public IActionResult Index()
        {
            var productList = GetProducts();
            return View(productList);
        }

        [HttpPost]
        public IActionResult ExportExcel()
        {
            var products = GetProducts();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Products");

                worksheet.Cell(1, 1).Value = "Id";
                worksheet.Cell(1, 2).Value = "Product Name";
                worksheet.Cell(1, 3).Value = "Unit Price";
                worksheet.Cell(1, 4).Value = "Quantity";
                worksheet.Cell(1, 5).Value = "Total Price";

                for (int i = 0; i < products.Count; i++)
                {
                    var p = products[i];
                    worksheet.Cell(i + 2, 1).Value = p.Id;
                    worksheet.Cell(i + 2, 2).Value = p.ProductName;
                    worksheet.Cell(i + 2, 3).Value = p.UnitPrice;
                    worksheet.Cell(i + 2, 4).Value = p.Quantity;
                    worksheet.Cell(i + 2, 5).Value = p.UnitPrice * p.Quantity; 
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Report.xlsx");
                }
            }
        }
    }
}
