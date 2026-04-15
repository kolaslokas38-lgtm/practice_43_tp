using JournalApp.Data;
using JournalApp.Models;
using Microsoft.EntityFrameworkCore;

namespace JournalApp.Repositories
{
    public class StudentRepository
    {
        private readonly JournalDbContext _context;

        public StudentRepository(JournalDbContext context)
        {
            _context = context;
        }

        public Task<List<StudentModel>> GetAllAsync()
            => _context.Students.AsNoTracking().OrderBy(s => s.Name).ToListAsync();

        public Task<StudentModel?> GetByIdAsync(int id)
            => _context.Students.FirstOrDefaultAsync(s => s.Id == id);

        public async Task AddAsync(StudentModel student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(StudentModel student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (entity == null) return;
            _context.Students.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}

