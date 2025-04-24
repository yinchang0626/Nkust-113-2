using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Services;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 加入 MVC 服務
            builder.Services.AddControllersWithViews();

            // 註冊 Rescue 相關服務
            builder.Services.AddScoped<IRescueService, RescueService>();

            // 加入 DbContext（資料庫連線）
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // 例外處理與安全性設定
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(); // 確保能處理 wwwroot 中的靜態資源

            app.UseRouting();
            app.UseAuthorization();

            // 設定路由
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Rescue}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
