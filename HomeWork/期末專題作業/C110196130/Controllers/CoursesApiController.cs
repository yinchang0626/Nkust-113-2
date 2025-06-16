using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseSelectionSystem.Data;
using CourseSelectionSystem.Models;
using System;
using System.Linq;

namespace CourseSelectionSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesApiController : ControllerBase
    {
        private readonly AppDbContext _db;
        public CoursesApiController(AppDbContext db) { _db = db; }

        // GET: api/CoursesApi
        [HttpGet]
        public IActionResult GetCourses()
        {
            var courses = _db.Courses.ToList();
            return Ok(courses);
        }

        // GET: api/CoursesApi/5
        [HttpGet("{id}")]
        public IActionResult GetCourse(int id)
        {
            var course = _db.Courses.FirstOrDefault(c => c.CourseId == id);
            if (course == null) return NotFound();
            return Ok(course);
        }

        // POST: api/CoursesApi
        [HttpPost]
        public IActionResult CreateCourse([FromBody] Course model)
        {
            _db.Courses.Add(model);
            _db.SaveChanges();
            return Ok(model);
        }

        // PUT: api/CoursesApi/5
        [HttpPut("{id}")]
        public IActionResult UpdateCourse(int id, [FromBody] Course model)
        {
            var course = _db.Courses.FirstOrDefault(c => c.CourseId == id);
            if (course == null) return NotFound();
            course.CourseName = model.CourseName;
            course.Description = model.Description;
            course.Instructor = model.Instructor;
            course.Price = model.Price;
            course.StartDate = model.StartDate;
            course.ImageUrl = model.ImageUrl;
            _db.SaveChanges();
            return Ok(course);
        }

        // DELETE: api/CoursesApi/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            var course = _db.Courses.FirstOrDefault(c => c.CourseId == id);
            if (course == null) return NotFound();
            _db.Courses.Remove(course);
            _db.SaveChanges();
            return Ok();
        }

        // POST: api/CoursesApi/Register
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] Enrollment model)
        {
            // model.UserId 與 model.CourseId 需由前端傳入
            if (!_db.Courses.Any(c => c.CourseId == model.CourseId))
                return BadRequest("課程不存在");
            if (!_db.Users.Any(u => u.UserId == model.UserId))
                return BadRequest("使用者不存在");
            if (_db.Enrollments.Any(e => e.UserId == model.UserId && e.CourseId == model.CourseId))
                return BadRequest("已註冊過此課程");
            model.EnrollmentDate = DateTime.Now;
            _db.Enrollments.Add(model);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
