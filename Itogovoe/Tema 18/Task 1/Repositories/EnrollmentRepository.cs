using Microsoft.EntityFrameworkCore;
using Task_1.Data;
using Task_1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_1.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Enrollment>> GetAllAsync()
        {
            return await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToListAsync();
        }

        public async Task<List<Enrollment>> GetByStudentIdAsync(int studentId)
        {
            return await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToListAsync();
        }

        public async Task<Enrollment?> GetByIdAsync(int id)
        {
            return await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(Enrollment enrollment)
        {
            await _context.Enrollments.AddAsync(enrollment);
        }

        public Task UpdateAsync(Enrollment enrollment)
        {
            _context.Enrollments.Update(enrollment);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var enrollment = await GetByIdAsync(id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}