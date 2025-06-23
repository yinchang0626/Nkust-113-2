using Microsoft.AspNetCore.Mvc;
using CourseSelectionSystem.Models;
using CourseSelectionSystem.Data;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CourseSelectionSystem.Controllers
{
    public class CoursesController : BaseController
    {
        private readonly AppDbContext _db;
        public CoursesController(AppDbContext db)
        {
            _db = db;
        }

        private void ImportCoursesToDb()
        {
            if (_db.Courses.Any()) return; // 已匯入過
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "nkust", "course.txt");
            if (!System.IO.File.Exists(filePath)) return;
            var lines = System.IO.File.ReadAllLines(filePath, Encoding.UTF8);
            for (int i = 1; i < lines.Length; i++) // 跳過標題列
            {
                var cols = lines[i].Contains(",") ? lines[i].Split(',') : lines[i].Split('\t');
                if (cols.Length < 12) continue;
                var course = new Course
                {
                    CourseId = int.TryParse(cols[0], out int cid) ? cid : i,
                    CourseName = cols[3].Trim(),
                    Description = string.Join("|", cols.Select(x => x.Trim())),
                    Instructor = cols.Length > 5 ? cols[5].Trim() : null,
                    Price = cols.Length > 6 && decimal.TryParse(cols[6], out var price) ? price : null,
                    StartDate = cols.Length > 7 && DateTime.TryParse(cols[7], out var dt) ? dt : null,
                    ImageUrl = cols.Length > 8 ? cols[8].Trim() : null
                };
                if (!_db.Courses.Any(c => c.CourseId == course.CourseId))
                    _db.Courses.Add(course);
            }
            _db.SaveChanges();
        }

        private void SyncCoursesFromText()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "nkust", "course.txt");
            if (!System.IO.File.Exists(filePath)) return;
            var lines = System.IO.File.ReadAllLines(filePath, Encoding.UTF8);
            var txtCourseIds = new HashSet<int>();
            for (int i = 1; i < lines.Length; i++) // 跳過標題列
            {
                var cols = lines[i].Contains(",") ? lines[i].Split(',') : lines[i].Split('\t');
                if (cols.Length < 12) continue;
                int courseId = int.TryParse(cols[0], out int cid) ? cid : i;
                txtCourseIds.Add(courseId);
                var course = _db.Courses.FirstOrDefault(c => c.CourseId == courseId);
                if (course == null)
                {
                    // 新增課程
                    course = new Course
                    {
                        CourseId = courseId,
                        CourseName = cols[3].Trim(),
                        Description = string.Join("|", cols.Select(x => x.Trim())),
                        Instructor = cols.Length > 5 ? cols[5].Trim() : null,
                        Price = cols.Length > 6 && decimal.TryParse(cols[6], out var price) ? price : null,
                        StartDate = cols.Length > 7 && DateTime.TryParse(cols[7], out var dt) ? dt : null,
                        ImageUrl = cols.Length > 8 ? cols[8].Trim() : null
                    };
                    _db.Courses.Add(course);
                }
                else
                {
                    // 更新課程內容（如有異動）
                    course.CourseName = cols[3].Trim();
                    course.Description = string.Join("|", cols.Select(x => x.Trim()));
                    course.Instructor = cols.Length > 5 ? cols[5].Trim() : null;
                    course.Price = cols.Length > 6 && decimal.TryParse(cols[6], out var price) ? price : null;
                    course.StartDate = cols.Length > 7 && DateTime.TryParse(cols[7], out var dt) ? dt : null;
                    course.ImageUrl = cols.Length > 8 ? cols[8].Trim() : null;
                }
            }
            // 刪除資料庫中不存在於 txt 的課程
            var toDelete = _db.Courses.Where(c => !txtCourseIds.Contains(c.CourseId)).ToList();
            if (toDelete.Any())
            {
                _db.Courses.RemoveRange(toDelete);
            }
            _db.SaveChanges();
        }

        public IActionResult Index(string search, string sort, int page = 1)
        {
            SyncCoursesFromText();
            var courses = _db.Courses.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                courses = courses.Where(c => (c.CourseName ?? "").Contains(search) || (c.Description ?? "").Contains(search));
            }
            switch (sort)
            {
                case "name":
                    courses = courses.OrderBy(c => c.CourseName);
                    break;
                default:
                    courses = courses.OrderBy(c => c.CourseId);
                    break;
            }

            int pageSize = 10;
            int totalCourses = courses.Count();
            int totalPages = (int)Math.Ceiling((double)totalCourses / pageSize);

            courses = courses.Skip((page - 1) * pageSize).Take(pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(courses.ToList());
        }

        public Course? GetCourseById(int id)
        {
            return _db.Courses.FirstOrDefault(c => c.CourseId == id);
        }
    }
}
