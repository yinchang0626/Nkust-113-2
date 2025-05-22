using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<CardAccessGrant> CardAccessGrants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Card entity
            modelBuilder.Entity<Card>(entity =>
            {
                entity.ToTable("Cards");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.DisplayName).IsRequired().HasMaxLength(128);
                entity.Property(e => e.CardNumber).IsRequired().HasMaxLength(64);
                entity.Property(e => e.MemberName).IsRequired().HasMaxLength(64);
                entity.Property(e => e.Remark).HasMaxLength(256);
            });

            // Configure Device entity
            modelBuilder.Entity<Device>(entity =>
            {
                entity.ToTable("Devices");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.DisplayName).IsRequired().HasMaxLength(128);
                entity.Property(e => e.DeviceCode).IsRequired().HasMaxLength(64);
                entity.Property(e => e.RemoteId).IsRequired().HasMaxLength(64);
                entity.Property(e => e.IpAddress).IsRequired().HasMaxLength(15);
            });

            // Configure CardAccessGrant entity
            modelBuilder.Entity<CardAccessGrant>(entity =>
            {
                entity.ToTable("CardAccessGrants");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Remark).HasMaxLength(256);
                
                // Define relationships
                entity.HasOne(d => d.Card)
                      .WithMany(p => p.AccessGrants)
                      .HasForeignKey(d => d.CardId)
                      .OnDelete(DeleteBehavior.Cascade);
                
                entity.HasOne(d => d.Device)
                      .WithMany(p => p.AccessGrants)
                      .HasForeignKey(d => d.DeviceId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
