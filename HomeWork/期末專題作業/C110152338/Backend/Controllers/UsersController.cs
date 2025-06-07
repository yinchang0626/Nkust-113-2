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
            return Ok(new { user.Username, user.Role, IsAdmin = user.IsAdmin });
        }

        public class LoginRequest
        {
            public required string Username { get; set; }
            public required string Password { get; set; }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest req)
        {
            var user = new User { Username = req.Username, PasswordHash = req.PasswordHash, Role = "user", Email = "", IsAdmin = req.IsAdmin };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new { user.Username, user.Role, IsAdmin = user.IsAdmin });
        }

        public class RegisterRequest
        {
            public required string Username { get; set; }
            public required string PasswordHash { get; set; }
            public bool IsAdmin { get; set; } = false;
        }

        public class ResetPasswordRequest
        {
            public required string Username { get; set; }
            public required string ResetKey { get; set; }
            public required string NewPassword { get; set; }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest req)
        {
            // 只允許金鑰正確才可重設密碼
            if (req.ResetKey != "kerong2002")
                return Unauthorized("Invalid reset key");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == req.Username);
            if (user == null)
                return NotFound("User not found");

            user.PasswordHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(req.NewPassword));
            await _context.SaveChangesAsync();
            return Ok(new { user.Username, message = "Password reset successful" });
        }
    }
}