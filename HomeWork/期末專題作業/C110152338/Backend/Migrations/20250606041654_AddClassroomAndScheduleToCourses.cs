using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace OnlineCoursePlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddClassroomAndScheduleToCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Classroom",
                table: "Courses",
                type: "nvarchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Schedule",
                table: "Courses",
                type: "nvarchar(255)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Classroom",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Schedule",
                table: "Courses");
        }
    }
}
