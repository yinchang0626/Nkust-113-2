using C110152310.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace C110152310.Controllers
{
    public class CoursesController : Microsoft.AspNetCore.Mvc.Controller
    {
        // 模擬資料庫
        private static List<Course> _courses = new List<Course>
        {
            new Course { Id = 1, Name = "數學", Description = "基礎數學課程" },
            new Course { Id = 2, Name = "英文", Description = "基礎英文課程" },
            new Course { Id = 3, Name = "程式設計", Description = "C# 程式設計入門" }
        };
        private static List<CourseSelection> _selections = new List<CourseSelection>();

        // GET: Courses
        public ActionResult Index()
        {
            return View(_courses);
        }

        // GET: Courses/Details/5
        public ActionResult Details(int id)
        {
            var course = _courses.FirstOrDefault(c => c.Id == id);
            if (course == null) return NotFound();
            return View(course);
        }

        // GET: Courses/Select/5
        public ActionResult Select(int id)
        {
            // 假設一律用同一個 UserId
            string userId = "demo-user";
            if (!_selections.Any(s => s.CourseId == id && s.UserId == userId))
            {
                _selections.Add(new CourseSelection { CourseId = id, UserId = userId, Course = _courses.First(c => c.Id == id) });
            }
            return RedirectToAction("MyCourses");
        }

        // GET: Courses/Drop/5
        public ActionResult Drop(int id)
        {
            string userId = "demo-user";
            var selection = _selections.FirstOrDefault(s => s.CourseId == id && s.UserId == userId);
            if (selection != null)
            {
                _selections.Remove(selection);
            }
            return RedirectToAction("MyCourses");
        }

        // GET: Courses/MyCourses
        public ActionResult MyCourses()
        {
            string userId = "demo-user";
            var myCourses = _selections.Where(s => s.UserId == userId).ToList();
            return View(myCourses);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                course.Id = _courses.Max(c => c.Id) + 1;
                _courses.Add(course);
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int id)
        {
            var course = _courses.FirstOrDefault(c => c.Id == id);
            if (course == null) return NotFound();
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var course = _courses.FirstOrDefault(c => c.Id == id);
            if (course != null)
            {
                _courses.Remove(course);
            }
            return RedirectToAction("Index");
        }
    }
}
