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
            var courses = new List<Course>
            {
                new Course { CourseName = "物理(二)", Description = "This course covers advanced topics in physics, including mechanics, heat, and sound.", Instructor = "謝東利", Price = 1000m, StartDate = new DateTime(2025, 9, 1), Classroom = "育102", Schedule = "(二)2-4" },
                new Course { CourseName = "微積分(二)", Description = "This course focuses on integration and differential equations in calculus.", Instructor = "朱紹儀", Price = 1000m, StartDate = new DateTime(2025, 9, 1), Classroom = "育102", Schedule = "(三)2-4" },
                new Course { CourseName = "電子學(一)", Description = "Introduction to semiconductor devices and electronic circuits.", Instructor = "丁信文", Price = 1000m, StartDate = new DateTime(2025, 9, 1), Classroom = "育502", Schedule = "(五)2-4" },
                new Course { CourseName = "電路學(一)", Description = "Analysis of DC and AC circuits, circuit theorems, and applications.", Instructor = "朱紹儀", Price = 1000m, StartDate = new DateTime(2025, 9, 1), Classroom = "資701", Schedule = "(四)5-7" },
                new Course { CourseName = "數位系統設計", Description = "Design and implementation of digital systems and logic circuits.", Instructor = "連志原", Price = 1000m, StartDate = new DateTime(2025, 9, 1), Classroom = "資809", Schedule = "(一)6-8" },
                new Course { CourseName = "計算機程式設計", Description = "Introduction to computer programming concepts and C/C++ language.", Instructor = "劉炳宏", Price = 1000m, StartDate = new DateTime(2025, 9, 1), Classroom = "資501A", Schedule = "(一)2-4" },
                new Course { CourseName = "中文閱讀與表達(二)", Description = "Advanced Chinese reading and expression skills.", Instructor = "蕭麗娟", Price = 1000m, StartDate = new DateTime(2025, 9, 1), Classroom = "育104", Schedule = "(二)5-6" },
                new Course { CourseName = "實用英文(二)", Description = "Practical English for communication and academic purposes.", Instructor = "王佩玲", Price = 1000m, StartDate = new DateTime(2025, 9, 1), Classroom = "育204", Schedule = "(二)7-8" },
                new Course { CourseName = "線性代數", Description = "Matrix theory, vector spaces, and linear transformations.", Instructor = "朱紹儀", Price = 1000m, StartDate = new DateTime(2025, 9, 1), Classroom = "資701", Schedule = "(三)7-9" },
                new Course { CourseName = "工程數學(二)", Description = "Mathematical methods for engineering applications.", Instructor = "潘天賜", Price = 1000m, StartDate = new DateTime(2025, 9, 1), Classroom = "育102", Schedule = "(四)6-8" },
                new Course { CourseName = "計算機網路", Description = "Fundamentals of computer networking and protocols.", Instructor = "謝欽旭", Price = 1000m, StartDate = new DateTime(2025, 9, 1), Classroom = "資809", Schedule = "(二)2-4" },
                new Course { CourseName = "視窗程式設計", Description = "Windows application development and GUI programming.", Instructor = "張財榮", Price = 1000m, StartDate = new DateTime(2025, 9, 1), Classroom = "資501A", Schedule = "(一)5-7" },
                new Course { CourseName = "資料結構", Description = "Data structures and algorithms for efficient computing.", Instructor = "劉炳宏", Price = 1000m, StartDate = new DateTime(2025, 9, 1), Classroom = "資701", Schedule = "(三)2-4" },
                new Course { CourseName = "FPGA系統設計實務", Description = "Practical design and implementation of FPGA systems.", Instructor = "黃冠渝", Price = 1000m, StartDate = new DateTime(2025, 9, 1), Classroom = "資809", Schedule = "(四)2-4" },
                new Course { CourseName = "實用英文(四)", Description = "Advanced practical English for professional use.", Instructor = "王佩玲", Price = 1000m, StartDate = new DateTime(2025, 9, 1), Classroom = "育204", Schedule = "(二)5-6" },
                new Course { CourseName = "數值分析", Description = "Numerical methods and analysis for scientific computing.", Instructor = "宋杭融", Price = 1000m, StartDate = new DateTime(2025, 9, 1), Classroom = "資405", Schedule = "(五)5-7" },
                new Course { CourseName = "作業系統", Description = "Principles and design of modern operating systems.", Instructor = "張財榮", Price = 1000m, StartDate = new DateTime(2025, 9, 1), Classroom = "資501A", Schedule = "(二)2-4" },
                new Course { CourseName = "人工智慧導論", Description = "Introduction to artificial intelligence concepts and applications.", Instructor = "張財榮", Price = 1000m, StartDate = new DateTime(2025, 9, 1), Classroom = "資501A", Schedule = "(四)2-4" },
                new Course { CourseName = "工程英文(二)", Description = "English for engineering and technical communication.", Instructor = "王怡人", Price = 1000m, StartDate = new DateTime(2025, 9, 1), Classroom = "資809", Schedule = "(一)2-4" },
                new Course { CourseName = "多媒體系統", Description = "Multimedia systems and applications in computing.", Instructor = "陳聰毅", Price = 1000m, StartDate = new DateTime(2025, 9, 1), Classroom = "資405", Schedule = "(四)2-4" },
                new Course { CourseName = "FPGA應用設計", Description = "FPGA application design and development techniques.", Instructor = "黃冠渝", Price = 1000m, StartDate = new DateTime(2025, 9, 1), Classroom = "資704", Schedule = "(五)1-4" }
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