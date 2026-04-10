using System.Collections.Generic;
using System.Threading.Tasks;
using Task_1.Models;

namespace Task_1.Repositories
{
    public interface IEnrollmentRepository
    {
        Task<List<Enrollment>> GetAllAsync();
        Task<List<Enrollment>> GetByStudentIdAsync(int studentId);
        Task<Enrollment?> GetByIdAsync(int id);
        Task AddAsync(Enrollment enrollment);
        Task UpdateAsync(Enrollment enrollment);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}