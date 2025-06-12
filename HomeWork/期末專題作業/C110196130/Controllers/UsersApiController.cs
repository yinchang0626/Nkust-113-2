using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseSelectionSystem.Data;
using CourseSelectionSystem.Models;
using System.Security.Cryptography;
using System.Text;

namespace CourseSelectionSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersApiController : ControllerBase
    {
        private readonly AppDbContext _db;
        public UsersApiController(AppDbContext db) { _db = db; }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User model)
        {
            if (_db.Users.Any(u => u.Username == model.Username))
                return BadRequest("帳號已存在");
            model.PasswordHash = HashPassword(model.PasswordHash);
            _db.Users.Add(model);
            _db.SaveChanges();
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User model)
        {
            var user = _db.Users.FirstOrDefault(u => u.Username == model.Username);
            if (user == null || user.PasswordHash != HashPassword(model.PasswordHash))
                return Unauthorized();
            return Ok(new { user.UserId, user.Username, user.Role });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _db.Users.Select(u => new { u.UserId, u.Username, u.Email, u.Role }).ToList();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _db.Users.Where(u => u.UserId == id).Select(u => new { u.UserId, u.Username, u.Email, u.Role }).FirstOrDefault();
            if (user == null) return NotFound();
            return Ok(user);
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
