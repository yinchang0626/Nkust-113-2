using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineCoursePlatform.Migrations
{
    public partial class SeedCourseData2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Courses");

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseName", "Classroom", "Schedule", "Description", "Instructor", "Price", "StartDate" },
                values: new object[,]
                {
                    { "程式語言實習(二)", "資001", "(三)7-8", "", "", 1000, DateTime.Now },
                    { "物理(二)", "育102", "(二)2-4", "", "", 1000, DateTime.Now },
                    { "微積分(二)", "育102", "(三)2-4", "", "", 1000, DateTime.Now },
                    { "電子學(一)", "育502", "(五)2-4", "", "", 1000, DateTime.Now },
                    { "電路學(一)", "資701", "(四)5-7", "", "", 1000, DateTime.Now },
                    { "數位系統設計", "資809", "(一)6-8", "", "", 1000, DateTime.Now },
                    { "計算機程式設計", "資501A", "(一)2-4", "", "", 1000, DateTime.Now },
                    { "中文閱讀與表達(二)", "育104", "(二)5-6", "", "", 1000, DateTime.Now },
                    { "實用英文(二)", "育204", "(二)7-8", "", "", 1000, DateTime.Now },
                    { "體育(二)", "", "(五)7-8", "", "", 0, DateTime.Now },
                    { "線性代數", "資701", "(三)7-9", "", "", 1000, DateTime.Now },
                    { "工程數學(二)", "育102", "(四)6-8", "", "", 1000, DateTime.Now },
                    { "計算機網路", "資809", "(二)2-4", "", "", 1000, DateTime.Now },
                    { "視窗程式設計", "資501A", "(一)5-7", "", "", 1000, DateTime.Now },
                    { "資料結構", "資701", "(三)2-4", "", "", 1000, DateTime.Now },
                    { "FPGA系統設計實務", "資809", "(四)2-4", "", "", 1000, DateTime.Now },
                    { "實用英文(四)", "育204", "(二)5-6", "", "", 1000, DateTime.Now },
                    { "數值分析", "資405", "(五)5-7", "", "", 1000, DateTime.Now },
                    { "作業系統", "資501A", "(二)2-4", "", "", 1000, DateTime.Now },
                    { "人工智慧導論", "資501A", "(四)2-4", "", "", 1000, DateTime.Now },
                    { "工程英文(二)", "資809", "(一)2-4", "", "", 1000, DateTime.Now },
                    { "專案實習", "", "", "", "", 0, DateTime.Now },
                    { "多媒體系統", "資405", "(四)2-4", "", "", 1000, DateTime.Now },
                    { "FPGA應用設計", "資704", "(五)1-4", "", "", 1000, DateTime.Now }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Courses");
        }
    }
}
