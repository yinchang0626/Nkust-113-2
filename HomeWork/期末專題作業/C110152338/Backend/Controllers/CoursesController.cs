using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<List<Course>> GetCourses()
        {
            // Add some sample courses
            var courses = new List<Course>
            {
                new Course { CourseName = "物理(二)", Description = "0896 物理(二). Instructor: 劉志益, Classroom: [教室], Time: [上課時間]. Covers mechanics, heat, and sound.", Instructor = "劉志益", Price = 100.00m, StartDate = new DateTime(2025, 9, 1) },
                new Course { CourseName = "微積分(二)", Description = "0897 微積分(二). Instructor: 丁信文, Classroom: [教室], Time: [上課時間]. Focuses on integration and differential equations.", Instructor = "丁信文", Price = 120.00m, StartDate = new DateTime(2025, 9, 1) },
                new Course { CourseName = "電子實習(一)", Description = "0898 電子實習(一). Instructor: 劉志益, Classroom: [教室], Time: [上課時間]. Hands-on experience with basic electronic circuits.", Instructor = "劉志益", Price = 90.00m, StartDate = new DateTime(2025, 9, 1) },
                new Course { CourseName = "電子學(一)", Description = "0899 電子學(一). Instructor: 劉淑白, Classroom: [教室], Time: [上課時間]. Introduction to semiconductor devices and circuits.", Instructor = "劉淑白", Price = 110.00m, StartDate = new DateTime(2025, 9, 1) },
                new Course { CourseName = "電路學(一)", Description = "0900 電路學(一). Instructor: 劉淑白, Classroom: [教室], Time: [上課時間]. Analysis of DC and AC circuits.", Instructor = "劉淑白", Price = 95.00m, StartDate = new DateTime(2025, 9, 1) },
                new Course { CourseName = "計算機程式設計", Description = "0901 計算機程式設計. Instructor: 潘天賜, Classroom: [教室], Time: [上課時間]. Introduction to programming using C++.", Instructor = "潘天賜", Price = 105.00m, StartDate = new DateTime(2025, 9, 1) },
                new Course { CourseName = "程式語言實習(二)", Description = "0902 程式語言實習(二). Instructor: 劉炳宏, Classroom: [教室], Time: [上課時間]. Practical exercises in programming languages.", Instructor = "劉炳宏", Price = 85.00m, StartDate = new DateTime(2025, 9, 1) }
            };
            return courses;
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return course;
        }

        // POST: api/Courses
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCourse", new { id = course.CourseId }, course);
        }

        // PUT: api/Courses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.CourseId)
            {
                return BadRequest();
            }
            _context.Entry(course).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> CourseExists(int id)
        {
            return await _context.Courses.AnyAsync(e => e.CourseId == id);
        }
    }
}