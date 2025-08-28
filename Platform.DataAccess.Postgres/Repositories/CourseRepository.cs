using Microsoft.EntityFrameworkCore;
using Platform.DataAccess.Postgres.Models;

namespace Platform.DataAccess.Postgres.Repositories;

public class CourseRepository
{
    private readonly PlatformDbContext _context;

    public CourseRepository(PlatformDbContext context)
    {
        _context = context;
    }

    public async Task<List<CourseEntity>> GetAllAsync()
    {
        return await _context.Courses
            .AsNoTracking()
            .OrderBy(c => c.Title)
            .ToListAsync();
    }
    
    public async Task<List<CourseEntity>> GetAllWithLessonsAsync()
    {
        return await _context.Courses
            .AsNoTracking()
            .Include(c => c.Lessons)
            .ToListAsync();
    }

    public async Task<CourseEntity?> GetByIdAsync(Guid id)
    {
        return await _context.Courses
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }
    
    public async Task<List<CourseEntity>> GetByFilterAsync(string title, decimal price)
    {
        var query = _context.Courses.AsNoTracking();

        if (!string.IsNullOrEmpty(title))
        {
            query = query.Where(c => c.Title.Contains(title));
        }
        
        if (decimal.IsPositive(price))
        {
            query = query.Where(c => c.Price <= price);
        }

        return await query.ToListAsync();
    }
    
    public async Task<List<CourseEntity>> GetByPageAsync(int page, int pageSize)
    {
        return await _context.Courses
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task AddAsync(Guid id, Guid authorId, string title, string description, decimal price)
    {
        var courseEntity = new CourseEntity
        {
            Id = id,
            AuthorId = authorId,
            Title = title,
            Description = description,
            Price = price
        };

        await _context.AddAsync(courseEntity);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(Guid id, Guid authorId, string title, string description, decimal price)
    {
        await _context.Courses
            .Where(c => c.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.Title, title)
                .SetProperty(c => c.Description, description)
                .SetProperty(c => c.Price, price));
    }

    public async Task DeleteAsync(Guid id)
    {
        await _context.Courses
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
    }
}