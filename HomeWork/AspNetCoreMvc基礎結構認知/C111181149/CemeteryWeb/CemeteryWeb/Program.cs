using CemeteryWeb.Services;
using CsvHelper;
using System.Globalization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICemeteryService, CemeteryService>();
builder.Services.AddSingleton<ICemeteryService, CemeteryService>();
builder.Services.AddScoped<ICareServiceService, CareServiceService>();
builder.Services.AddScoped<IRecheckCaseService, RecheckCaseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=RecheckCase}/{action=Index}/{id?}");

app.Run();