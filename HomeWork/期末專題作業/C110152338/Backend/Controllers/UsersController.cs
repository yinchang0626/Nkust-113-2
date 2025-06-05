using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == req.Username);
            if (user == null)
                return Unauthorized("User not found");
            var hash = Convert.ToBase64String(Encoding.UTF8.GetBytes(req.Password));
            if (user.PasswordHash != hash)
                return Unauthorized("Wrong password");
            return Ok(new { user.Username, user.Role, user.IsAdmin });
        }

        public class LoginRequest
        {
            public required string Username { get; set; }
            public required string Password { get; set; }
        }
    }
}