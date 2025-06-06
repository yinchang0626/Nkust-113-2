using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Backend.Models;
using System.Linq;

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

    // 課程種子資料（如需自 course.txt 匯入，請於此區塊實作）
    if (!context.Courses.Any())
    {
        context.Courses.AddRange(
            new Course { CourseName = "物理(二)", Description = "This course covers advanced topics in physics, including mechanics, heat, and sound.", Instructor = "劉志益", Classroom = "育501", Schedule = "(四)5-7", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "微積分(二)", Description = "This course focuses on integration and differential equations in calculus.", Instructor = "丁信文", Classroom = "資701", Schedule = "(二)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "電子實習(一)", Description = "Introduction to semiconductor devices and electronic circuits.", Instructor = "劉志益", Classroom = "資401", Schedule = "(一)5-8", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "電子學(一)", Description = "Introduction to semiconductor devices and electronic circuits.", Instructor = "劉淑白", Classroom = "育502", Schedule = "(五)5-7", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "電路學(一)", Description = "Analysis of DC and AC circuits, circuit theorems, and applications.", Instructor = "劉淑白", Classroom = "育502", Schedule = "(三)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "計算機程式設計", Description = "Introduction to computer programming concepts and C/C++ language.", Instructor = "潘天賜", Classroom = "資501A", Schedule = "(五)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "程式語言實習(二)", Description = "Practical exercises in programming languages.", Instructor = "劉炳宏", Classroom = "資001", Schedule = "(三)7-8", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "中文閱讀與表達(二)", Description = "Advanced Chinese reading and expression skills.", Instructor = "邵詩淳", Classroom = "育103", Schedule = "(二)5-6", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "實用英文(二)", Description = "Practical English for communication and academic purposes.", Instructor = "王佩玲", Classroom = "育204", Schedule = "(二)7-8", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "程式語言實習(二)", Description = "Practical exercises in programming languages.", Instructor = "劉炳宏", Classroom = "資001", Schedule = "(三)7-8", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "物理(二)", Description = "This course covers advanced topics in physics, including mechanics, heat, and sound.", Instructor = "邱建良", Classroom = "育502", Schedule = "(四)5-7", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "微積分(二)", Description = "This course focuses on integration and differential equations in calculus.", Instructor = "楊素華", Classroom = "育202", Schedule = "(一)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "電子實習(一)", Description = "Introduction to semiconductor devices and electronic circuits.", Instructor = "張萬榮", Classroom = "資401", Schedule = "(四)1-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "電子學(一)", Description = "Introduction to semiconductor devices and electronic circuits.", Instructor = "周肇基", Classroom = "育501", Schedule = "(三)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "電路學(一)", Description = "Analysis of DC and AC circuits, circuit theorems, and applications.", Instructor = "潘建源", Classroom = "育501", Schedule = "(五)5-7", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "計算機程式設計", Description = "Introduction to computer programming concepts and C/C++ language.", Instructor = "鄭瑞川", Classroom = "資001", Schedule = "(一)5-7", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "中文閱讀與表達(二)", Description = "Advanced Chinese reading and expression skills.", Instructor = "戴碧燕", Classroom = "育101", Schedule = "(二)5-6", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "實用英文(二)", Description = "Practical English for communication and academic purposes.", Instructor = "王佩玲", Classroom = "育204", Schedule = "(二)7-8", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "程式語言實習(二)", Description = "Practical exercises in programming languages.", Instructor = "劉炳宏", Classroom = "資001", Schedule = "(三)7-8", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "物理(二)", Description = "This course covers advanced topics in physics, including mechanics, heat, and sound.", Instructor = "謝東利", Classroom = "育102", Schedule = "(二)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "微積分(二)", Description = "This course focuses on integration and differential equations in calculus.", Instructor = "朱紹儀", Classroom = "育102", Schedule = "(三)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "電子學(一)", Description = "Introduction to semiconductor devices and electronic circuits.", Instructor = "丁信文", Classroom = "育502", Schedule = "(五)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "電路學(一)", Description = "Analysis of DC and AC circuits, circuit theorems, and applications.", Instructor = "朱紹儀", Classroom = "資701", Schedule = "(四)5-7", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "數位系統設計", Description = "Design and implementation of digital systems and logic circuits.", Instructor = "連志原", Classroom = "資809", Schedule = "(一)6-8", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "計算機程式設計", Description = "Introduction to computer programming concepts and C/C++ language.", Instructor = "劉炳宏", Classroom = "資501A", Schedule = "(一)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "中文閱讀與表達(二)", Description = "Advanced Chinese reading and expression skills.", Instructor = "蕭麗娟", Classroom = "育104", Schedule = "(二)5-6", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "實用英文(二)", Description = "Practical English for communication and academic purposes.", Instructor = "王佩玲", Classroom = "育204", Schedule = "(二)7-8", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "工程數學(二)", Description = "Mathematical methods for engineering applications.", Instructor = "江柏叡", Classroom = "育502", Schedule = "(二)7-9", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "近代物理", Description = "Modern physics concepts and theories.", Instructor = "洪冠明", Classroom = "育202", Schedule = "(三)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "電子材料", Description = "Electronic materials and their properties.", Instructor = "劉志益", Classroom = "育202", Schedule = "(五)5-7", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "電子實習(三)", Description = "Advanced practical exercises in electronics.", Instructor = "邱建良", Classroom = "資401", Schedule = "(五)1-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "電子學(三)", Description = "Advanced topics in electronic circuits and systems.", Instructor = "王鴻猷", Classroom = "育501", Schedule = "(四)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "電磁學", Description = "Electromagnetic fields and waves.", Instructor = "薛丁仁", Classroom = "育502", Schedule = "(一)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "線性代數", Description = "Matrix theory, vector spaces, and linear transformations.", Instructor = "朱紹儀", Classroom = "資701", Schedule = "(三)7-9", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "感測器實務", Description = "Practical applications of sensors and transducers.", Instructor = "江柏叡", Classroom = "資501A", Schedule = "(四)5-7", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "計算機組織與設計", Description = "Computer organization and design principles.", Instructor = "丁信文", Classroom = "育102", Schedule = "(一)5-7", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "實用英文(四)", Description = "Advanced practical English for professional use.", Instructor = "王佩玲", Classroom = "育204", Schedule = "(二)5-6", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "線性代數", Description = "Matrix theory, vector spaces, and linear transformations.", Instructor = "朱紹儀", Classroom = "資701", Schedule = "(三)7-9", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "工程數學(二)", Description = "Mathematical methods for engineering applications.", Instructor = "謝東利", Classroom = "育502", Schedule = "(一)5-7", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "電子實習(三)", Description = "Advanced practical exercises in electronics.", Instructor = "鄭瑞川", Classroom = "資405", Schedule = "(四)5-8", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "電子學(三)", Description = "Advanced topics in electronic circuits and systems.", Instructor = "周肇基", Classroom = "育502", Schedule = "(二)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "電磁學", Description = "Electromagnetic fields and waves.", Instructor = "周肇基", Classroom = "育502", Schedule = "(四)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "實用英文(四)", Description = "Advanced practical English for professional use.", Instructor = "王佩玲", Classroom = "育204", Schedule = "(二)5-6", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "線性代數", Description = "Matrix theory, vector spaces, and linear transformations.", Instructor = "朱紹儀", Classroom = "資701", Schedule = "(三)7-9", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "工程數學(二)", Description = "Mathematical methods for engineering applications.", Instructor = "潘天賜", Classroom = "育102", Schedule = "(四)6-8", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "計算機網路", Description = "Fundamentals of computer networking and protocols.", Instructor = "謝欽旭", Classroom = "資809", Schedule = "(二)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "視窗程式設計", Description = "Windows application development and GUI programming.", Instructor = "張財榮", Classroom = "資501A", Schedule = "(一)5-7", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "資料結構", Description = "Data structures and algorithms for efficient computing.", Instructor = "劉炳宏", Classroom = "資701", Schedule = "(三)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "FPGA系統設計實務", Description = "Practical design and implementation of FPGA systems.", Instructor = "黃冠渝", Classroom = "資809", Schedule = "(四)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "實用英文(四)", Description = "Advanced practical English for professional use.", Instructor = "王佩玲", Classroom = "育204", Schedule = "(二)5-6", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "半導體元件", Description = "Semiconductor devices and their characteristics.", Instructor = "楊素華", Classroom = "育202", Schedule = "(四)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "數值分析", Description = "Numerical methods and analysis for scientific computing.", Instructor = "宋杭融", Classroom = "資405", Schedule = "(五)5-7", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "類比積體電路設計", Description = "Design of analog integrated circuits.", Instructor = "王鴻猷", Classroom = "資504", Schedule = "(三)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "VLSI設計實務", Description = "Practical design and implementation of VLSI systems.", Instructor = "謝慶發", Classroom = "資501A", Schedule = "(三)7-9", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "生醫電子儀表與量測", Description = "Biomedical electronic instruments and measurements.", Instructor = "江柏叡,蔡正才", Classroom = "資504", Schedule = "(二)5-7", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "數位系統設計", Description = "Design and implementation of digital systems.", Instructor = "蔣元隆", Classroom = "資704", Schedule = "(四)5-7", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "數值分析", Description = "Numerical methods and analysis for scientific computing.", Instructor = "宋杭融", Classroom = "資405", Schedule = "(五)5-7", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "VLSI設計實務", Description = "Practical design and implementation of VLSI systems.", Instructor = "謝慶發", Classroom = "資501A", Schedule = "(三)7-9", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "數位訊號處理", Description = "Digital signal processing techniques and applications.", Instructor = "鄭瑞川", Classroom = "資001", Schedule = "(二)5-7", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "數位影像處理", Description = "Digital image processing techniques and applications.", Instructor = "張俊傑", Classroom = "資704", Schedule = "(三)7-9", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "人工智慧導論", Description = "Introduction to artificial intelligence concepts.", Instructor = "張財榮", Classroom = "資501A", Schedule = "(四)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "手機平台實務", Description = "Practical development for mobile platforms.", Instructor = "翁欲盛", Classroom = "資809", Schedule = "(五)1-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "數值分析", Description = "Numerical methods and analysis for scientific computing.", Instructor = "宋杭融", Classroom = "資405", Schedule = "(五)5-7", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "作業系統", Description = "Principles and design of operating systems.", Instructor = "張財榮", Classroom = "資501A", Schedule = "(二)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "人工智慧導論", Description = "Introduction to artificial intelligence concepts.", Instructor = "張財榮", Classroom = "資501A", Schedule = "(四)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "雷射工程", Description = "Laser engineering principles and applications.", Instructor = "藍仁鴻", Classroom = "育405", Schedule = "(四)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "薄膜技術", Description = "Thin film technology and applications.", Instructor = "劉宜鑫", Classroom = "育202", Schedule = "(四)6-8", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "工程英文(二)", Description = "English for engineering and technical communication.", Instructor = "王怡人", Classroom = "資809", Schedule = "(一)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "太陽能元件", Description = "Solar cell devices and technology.", Instructor = "李俊哲", Classroom = "育505", Schedule = "(五)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "材料科學與應用(二)", Description = "Materials science and engineering applications.", Instructor = "蔡明峯", Classroom = "資405", Schedule = "(三)7-9", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "無線感測網路實務", Description = "Practical implementation of wireless sensor networks.", Instructor = "洪盟峯", Classroom = "資809", Schedule = "(四)5-8", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "工程英文(二)", Description = "English for engineering and technical communication.", Instructor = "王怡人", Classroom = "資809", Schedule = "(一)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "無線感測網路實務", Description = "Practical implementation of wireless sensor networks.", Instructor = "洪盟峯", Classroom = "資809", Schedule = "(四)5-8", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "工程英文(二)", Description = "English for engineering and technical communication.", Instructor = "王怡人", Classroom = "資809", Schedule = "(一)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "多媒體系統", Description = "Multimedia systems and applications.", Instructor = "陳聰毅", Classroom = "資405", Schedule = "(四)2-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) },
            new Course { CourseName = "FPGA應用設計", Description = "FPGA application design and development.", Instructor = "黃冠渝", Classroom = "資704", Schedule = "(五)1-4", Price = 1000m, StartDate = new DateTime(2025, 9, 1) }
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