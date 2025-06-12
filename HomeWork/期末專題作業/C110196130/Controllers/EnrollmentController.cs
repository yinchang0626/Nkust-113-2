using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CourseSelectionSystem.Models;
using CourseSelectionSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CourseSelectionSystem.Controllers
{
    public class EnrollmentController : BaseController
    {
        private readonly AppDbContext _db;
        public EnrollmentController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");
            var enrollments = _db.Enrollments.Include(e => e.Course)
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.EnrollmentDate)
                .ToList();
            return View(enrollments);
        }

        [HttpPost]
        public IActionResult Add(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");
            var course = _db.Courses.FirstOrDefault(c => c.CourseId == id);
            if (course == null) return NotFound();
            if (!_db.Enrollments.Any(e => e.UserId == userId && e.CourseId == id))
            {
                _db.Enrollments.Add(new Enrollment { UserId = userId.Value, CourseId = id, EnrollmentDate = DateTime.Now });
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");
            var enrollment = _db.Enrollments.FirstOrDefault(e => e.UserId == userId && e.CourseId == id);
            if (enrollment != null)
            {
                _db.Enrollments.Remove(enrollment);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Timetable()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");
            var enrollments = _db.Enrollments.Include(e => e.Course)
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.EnrollmentDate)
                .ToList();
            return View(enrollments);
        }
    }
}
