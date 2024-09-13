using JobApplicationAPIs.Data.DataModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationAPIs.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Users>().HasData(
                new { Id = 1, UserName = "admin", Password = "admin", Role = "Admin", RefreshToken = "asdfghjklasdfghjkl" },
                new { Id = 2, UserName = "manager", Password = "manager", Role = "Manager", RefreshToken = "asdfghjklasdfghjkl" },
                new { Id = 3, UserName = "user", Password = "user", Role = "User", RefreshToken = "asdfghjklasdfghjkl" }
            );
        }

        public DbSet<BasicDetails> BasicDetails { get; set; }
        public DbSet<EducationDetails> EducationDetails { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<Languages> Languages { get; set; }
        public DbSet<Technologies> Technologies { get; set; }
        public DbSet<Preferences> Preferences { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
