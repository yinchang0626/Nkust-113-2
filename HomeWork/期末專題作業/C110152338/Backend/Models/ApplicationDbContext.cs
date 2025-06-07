using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public required DbSet<Course> Courses { get; set; }
        public required DbSet<Enrollment> Enrollments { get; set; }
        public new DbSet<User> Users { get; set; }
    }
}
