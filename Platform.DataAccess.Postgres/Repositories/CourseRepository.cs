namespace Platform.DataAccess.Postgres.Repositories;

public class CourseRepository
{
    private readonly PlatformDbContext _context;

    public CourseRepository(PlatformDbContext context)
    {
        _context = context;
    }
}