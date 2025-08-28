using Microsoft.EntityFrameworkCore;
using Platform.DataAccess.Postgres.Configurations;
using Platform.DataAccess.Postgres.Models;

namespace Platform.DataAccess.Postgres;

public class PlatformDbContext : DbContext
{
    public PlatformDbContext(DbContextOptions<PlatformDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<CourseEntity> Courses { get; set; }
    public DbSet<LessonEntity> Lessons { get; set; }
    public DbSet<AuthorEntity> Authors { get; set; }
    public DbSet<StudentEntity> Student { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        modelBuilder.ApplyConfiguration(new AuthorConfiguration());
        modelBuilder.ApplyConfiguration(new LessonConfiguration());
    }
}