using Microsoft.EntityFrameworkCore;
using Platform.DataAccess.Postgres.Models;

namespace Platform.DataAccess.Postgres.Repositories;

public class LessonRepository
{
    private readonly PlatformDbContext _context;

    public LessonRepository(PlatformDbContext context)
    {
        _context = context;
    }

    public async Task AddLessonAsync(Guid courseId, LessonEntity lesson)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId)
            ?? throw new Exception();
        
        course.Lessons.Add(lesson);
        
        await _context.SaveChangesAsync();
    }
}