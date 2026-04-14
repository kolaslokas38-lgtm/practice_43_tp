using JournalApp.Data;
using JournalApp.Models;
using Microsoft.EntityFrameworkCore;

namespace JournalApp.Repositories
{
    public class CourseRepository
    {
        private readonly JournalDbContext _context;

        public CourseRepository(JournalDbContext context)
        {
            _context = context;
        }

        public Task<List<CourseModel>> GetAllAsync()
            => _context.Courses.AsNoTracking().OrderBy(c => c.Name).ToListAsync();

        public Task<CourseModel?> GetByIdAsync(int id)
            => _context.Courses.FirstOrDefaultAsync(c => c.Id == id);

        public async Task AddAsync(CourseModel course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CourseModel course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
            if (entity == null) return;
            _context.Courses.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}

