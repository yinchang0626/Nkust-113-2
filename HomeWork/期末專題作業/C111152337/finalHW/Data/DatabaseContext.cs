using Microsoft.EntityFrameworkCore;

using finalHW.Models;
namespace finalHW.Data
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        public DbSet<DataContent> Datas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DataContent>().HasData(
                new DataContent
                {
                    Id = 1,
                    FirstName ="YAN",
                    LastName="YAN",
                    Email="yanyan@gmail.com",
                    Gender="Female",
                    CompanyName="TSMC"
                },
                 new DataContent
                 {
                     Id = 2,
                     FirstName = "YAN",
                     LastName = "LING",
                     Email = "yanling@gmail.com",
                     Gender = "Female",
                     CompanyName = "ASE"
                 });
        }

    }
}
