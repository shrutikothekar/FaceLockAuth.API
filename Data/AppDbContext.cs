using FaceLockAuth.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FaceLockAuth.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.FaceDescriptor)
                .HasConversion(
                    v => string.Join(",", v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                          .Select(float.Parse)
                          .ToArray()
                );
        }


        public DbSet<Models.User> Users { get; set; }
    }
}
