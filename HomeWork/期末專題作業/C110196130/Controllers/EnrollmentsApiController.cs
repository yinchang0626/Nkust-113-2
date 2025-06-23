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
    public class EnrollmentsApiController : ControllerBase
    {
        private readonly AppDbContext _db;
        public EnrollmentsApiController(AppDbContext db) { _db = db; }

        [HttpGet("{userId}")]
        public IActionResult GetEnrollments(int userId)
        {
            var enrollments = _db.Enrollments
                .Where(e => e.UserId == userId)
                .Include(e => e.Course)
                .ToList();
            return Ok(enrollments);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] Enrollment model)
        {
            if (_db.Enrollments.Any(e => e.UserId == model.UserId && e.CourseId == model.CourseId))
                return BadRequest("已註冊過此課程");
            model.EnrollmentDate = DateTime.Now;
            _db.Enrollments.Add(model);
            _db.SaveChanges();
            return Ok();
        }

        [HttpPost("remove")]
        public IActionResult Remove([FromBody] Enrollment model)
        {
            var enrollment = _db.Enrollments.FirstOrDefault(e => e.UserId == model.UserId && e.CourseId == model.CourseId);
            if (enrollment == null) return NotFound();
            _db.Enrollments.Remove(enrollment);
            _db.SaveChanges();
            return Ok();
        }
    }
}
