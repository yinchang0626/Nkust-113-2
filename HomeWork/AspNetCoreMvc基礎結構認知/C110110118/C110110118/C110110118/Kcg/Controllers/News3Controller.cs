using Kcg.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;

namespace Kcg.Controllers
{
    public class News3Controller : Controller
    {
        private readonly string _path;

        public News3Controller(IWebHostEnvironment webHostEnvironment)
        {
            // 儲存圖片到 wwwroot/images
            _path = Path.Combine(webHostEnvironment.WebRootPath, "images");

            // 確保資料夾存在
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
        }

        FileInfo[] GetFiles()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(_path);
            FileInfo[] files = directoryInfo.GetFiles();
            return files;
        }

        public IActionResult Index()
        {
            return View(GetFiles());
        }
        [HttpPost]
        public async Task< IActionResult>Index(IFormFile formFile)
        {
            if (formFile != null) {
                if(formFile.Length > 0)
                {
                    string savePath = $@"{_path}\{formFile.FileName}";
                    using (var steam=new FileStream(savePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(steam);
                    }
                }
            }
            return View(GetFiles());
        }
    }
}
