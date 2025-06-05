using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Backend.Models;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5100");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Apply migrations
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();

    // Seed the database
    if (!context.Courses.Any())
    {
        context.Courses.AddRange(
            new Course { CourseName = "Math 101", Description = "Introduction to Mathematics", Instructor = "John Doe", Price = 99.99m, StartDate = DateTime.Now },
            new Course { CourseName = "English 101", Description = "Introduction to English Literature", Instructor = "Jane Smith", Price = 79.99m, StartDate = DateTime.Now },
            new Course { CourseName = "物理(二)", Description = "四子一甲 222C00135 物理(二) by 劉志益", Instructor = "劉志益", Price = 89.99m, StartDate = DateTime.Now },
            new Course { CourseName = "微積分(二", Description = "四子一甲 222C00140 微積分(二 by 丁信文", Instructor = "丁信文", Price = 89.99m, StartDate = DateTime.Now },
            new Course { CourseName = "電子實習(", Description = "四子一甲 222C00101 電子實習( by 劉志益", Instructor = "劉志益", Price = 89.99m, StartDate = DateTime.Now },
            new Course { CourseName = "電子學(一", Description = "四子一甲 222C00165 電子學(一 by 劉淑白", Instructor = "劉淑白", Price = 89.99m, StartDate = DateTime.Now },
            new Course { CourseName = "電路學(一", Description = "四子一甲 222C00156 電路學(一 by 劉淑白", Instructor = "劉淑白", Price = 89.99m, StartDate = DateTime.Now },
            new Course { CourseName = "計算機程式", Description = "四子一甲 222C00143 計算機程式 by 潘天賜", Instructor = "潘天賜", Price = 89.99m, StartDate = DateTime.Now },
            new Course { CourseName = "程式語言", Description = "四子一甲 222C10179 程式語言 by 劉炳宏", Instructor = "劉炳宏", Price = 89.99m, StartDate = DateTime.Now },
            new Course { CourseName = "中文閱讀與寫作(一)", Description = "四子一甲 041C00177 中文閱讀與寫作(一) by 邵詩淳", Instructor = "邵詩淳", Price = 89.99m, StartDate = DateTime.Now },
            new Course { CourseName = "實用英文(二)", Description = "四子一甲 043C00005 實用英文(二) by 王佩玲", Instructor = "王佩玲", Price = 89.99m, StartDate = DateTime.Now },
            new Course { CourseName = "體育(二)", Description = "四子一甲 050C00036 體育(二) by 黃盈晨", Instructor = "黃盈晨", Price = 89.99m, StartDate = DateTime.Now }
        );
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(); // 啟用 CORS
app.UseAuthorization();
    
app.MapControllers();
app.MapGet("/", () => "Online Course Platform API is running!");

app.Run();