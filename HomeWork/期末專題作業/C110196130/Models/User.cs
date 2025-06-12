using System.ComponentModel.DataAnnotations;

namespace CourseSelectionSystem.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string Role { get; set; } = "User";
    }
}
