using C110196130.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<CsvService>(); // 註冊 CsvService 作為 DI 服務

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 修改預設控制器為 DivorceRecordsController
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=DivorceRecords}/{action=Index}/{id?}"); // 設定預設路由到 DivorceRecordsController

app.Run();
