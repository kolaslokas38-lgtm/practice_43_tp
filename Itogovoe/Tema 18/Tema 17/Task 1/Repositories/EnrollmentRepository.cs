using JournalApp.Data;
using JournalApp.Models;
using Microsoft.EntityFrameworkCore;

namespace JournalApp.Repositories
{
    public class EnrollmentRepository
    {
        private readonly JournalDbContext _context;

        public EnrollmentRepository(JournalDbContext context)
        {
            _context = context;
        }

        public Task<List<EnrollmentModel>> GetAllAsync()
            => _context.Enrollments
                .AsNoTracking()
                .Include(e => e.Student)
                .Include(e => e.Course)
                .OrderBy(e => e.Student!.Name)
                .ThenBy(e => e.Course!.Name)
                .ToListAsync();

        public Task<List<EnrollmentModel>> GetByStudentIdAsync(int studentId)
            => _context.Enrollments
                .AsNoTracking()
                .Where(e => e.StudentId == studentId)
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToListAsync();

        public Task<EnrollmentModel?> GetByIdAsync(int id)
            => _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Include(e => e.Grades)
                .FirstOrDefaultAsync(e => e.Id == id);

        public Task<EnrollmentModel?> FindAsync(int studentId, int courseId)
            => _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Include(e => e.Grades)
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);

        public async Task AddAsync(EnrollmentModel enrollment)
        {
            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EnrollmentModel enrollment)
        {
            _context.Enrollments.Update(enrollment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Enrollments.FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null) return;
            _context.Enrollments.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}

