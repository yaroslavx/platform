using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Platform.DataAccess.Postgres.Configurations;
using Platform.DataAccess.Postgres.Models;

namespace Platform.DataAccess.Postgres;

public class PlatformDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public PlatformDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public DbSet<CourseEntity> Courses { get; set; }
    public DbSet<LessonEntity> Lessons { get; set; }
    public DbSet<AuthorEntity> Authors { get; set; }
    public DbSet<StudentEntity> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {  
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlatformDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_configuration.GetConnectionString(nameof(PlatformDbContext)))
            .EnableSensitiveDataLogging();
    }
}