using Crime.Models;
using Microsoft.EntityFrameworkCore;
namespace Crime.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<CrimeStat> CrimeStats { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
