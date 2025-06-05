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

    // 建立 admin 帳號（kerong/abc123）
    if (!context.Users.Any(u => u.Username == "kerong"))
    {
        var admin = new Backend.Models.User
        {
            Username = "kerong",
            // 密碼雜湊（簡單示範，實務請用 Identity）
            PasswordHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("abc123")),
            Email = "kerong@admin.com",
            Role = "Admin",
            IsAdmin = true
        };
        context.Users.Add(admin);
        context.SaveChanges();
    }

    // 課程種子資料（僅在資料表為空時寫入）
    if (!context.Courses.Any())
    {
        context.Courses.AddRange(
            new Course { CourseName = "物理(二)", Description = "This course covers advanced topics in physics, including mechanics, heat, and sound.", Instructor = "謝東利", Price = 1000, StartDate = new DateTime(2025, 9, 1), Classroom = "育102", Schedule = "(二)2-4" },
            new Course { CourseName = "微積分(二)", Description = "This course focuses on integration and differential equations in calculus.", Instructor = "朱紹儀", Price = 1000, StartDate = new DateTime(2025, 9, 1), Classroom = "育102", Schedule = "(三)2-4" },
            new Course { CourseName = "電子學(一)", Description = "Introduction to semiconductor devices and electronic circuits.", Instructor = "丁信文", Price = 1000, StartDate = new DateTime(2025, 9, 1), Classroom = "育502", Schedule = "(五)2-4" },
            new Course { CourseName = "電路學(一)", Description = "Analysis of DC and AC circuits, circuit theorems, and applications.", Instructor = "朱紹儀", Price = 1000, StartDate = new DateTime(2025, 9, 1), Classroom = "資701", Schedule = "(四)5-7" },
            new Course { CourseName = "數位系統設計", Description = "Design and implementation of digital systems and logic circuits.", Instructor = "連志原", Price = 1000, StartDate = new DateTime(2025, 9, 1), Classroom = "資809", Schedule = "(一)6-8" },
            new Course { CourseName = "計算機程式設計", Description = "Introduction to computer programming concepts and C/C++ language.", Instructor = "劉炳宏", Price = 1000, StartDate = new DateTime(2025, 9, 1), Classroom = "資501A", Schedule = "(一)2-4" },
            new Course { CourseName = "中文閱讀與表達(二)", Description = "Advanced Chinese reading and expression skills.", Instructor = "蕭麗娟", Price = 1000, StartDate = new DateTime(2025, 9, 1), Classroom = "育104", Schedule = "(二)5-6" },
            new Course { CourseName = "實用英文(二)", Description = "Practical English for communication and academic purposes.", Instructor = "王佩玲", Price = 1000, StartDate = new DateTime(2025, 9, 1), Classroom = "育204", Schedule = "(二)7-8" },
            new Course { CourseName = "線性代數", Description = "Matrix theory, vector spaces, and linear transformations.", Instructor = "朱紹儀", Price = 1000, StartDate = new DateTime(2025, 9, 1), Classroom = "資701", Schedule = "(三)7-9" },
            new Course { CourseName = "工程數學(二)", Description = "Mathematical methods for engineering applications.", Instructor = "潘天賜", Price = 1000, StartDate = new DateTime(2025, 9, 1), Classroom = "育102", Schedule = "(四)6-8" },
            new Course { CourseName = "計算機網路", Description = "Fundamentals of computer networking and protocols.", Instructor = "謝欽旭", Price = 1000, StartDate = new DateTime(2025, 9, 1), Classroom = "資809", Schedule = "(二)2-4" },
            new Course { CourseName = "視窗程式設計", Description = "Windows application development and GUI programming.", Instructor = "張財榮", Price = 1000, StartDate = new DateTime(2025, 9, 1), Classroom = "資501A", Schedule = "(一)5-7" },
            new Course { CourseName = "資料結構", Description = "Data structures and algorithms for efficient computing.", Instructor = "劉炳宏", Price = 1000, StartDate = new DateTime(2025, 9, 1), Classroom = "資701", Schedule = "(三)2-4" },
            new Course { CourseName = "FPGA系統設計實務", Description = "Practical design and implementation of FPGA systems.", Instructor = "黃冠渝", Price = 1000, StartDate = new DateTime(2025, 9, 1), Classroom = "資809", Schedule = "(四)2-4" },
            new Course { CourseName = "實用英文(四)", Description = "Advanced practical English for professional use.", Instructor = "王佩玲", Price = 1000, StartDate = new DateTime(2025, 9, 1), Classroom = "育204", Schedule = "(二)5-6" },
            new Course { CourseName = "數值分析", Description = "Numerical methods and analysis for scientific computing.", Instructor = "宋杭融", Price = 1000, StartDate = new DateTime(2025, 9, 1), Classroom = "資405", Schedule = "(五)5-7" },
            new Course { CourseName = "作業系統", Description = "Principles and design of modern operating systems.", Instructor = "張財榮", Price = 1000, StartDate = new DateTime(2025, 9, 1), Classroom = "資501A", Schedule = "(二)2-4" },
            new Course { CourseName = "人工智慧導論", Description = "Introduction to artificial intelligence concepts and applications.", Instructor = "張財榮", Price = 1000, StartDate = new DateTime(2025, 9, 1), Classroom = "資501A", Schedule = "(四)2-4" },
            new Course { CourseName = "工程英文(二)", Description = "English for engineering and technical communication.", Instructor = "王怡人", Price = 1000, StartDate = new DateTime(2025, 9, 1), Classroom = "資809", Schedule = "(一)2-4" },
            new Course { CourseName = "多媒體系統", Description = "Multimedia systems and applications in computing.", Instructor = "陳聰毅", Price = 1000, StartDate = new DateTime(2025, 9, 1), Classroom = "資405", Schedule = "(四)2-4" },
            new Course { CourseName = "FPGA應用設計", Description = "FPGA application design and development techniques.", Instructor = "黃冠渝", Price = 1000, StartDate = new DateTime(2025, 9, 1), Classroom = "資704", Schedule = "(五)1-4" }
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