using System.Collections.Generic;
using System.Threading.Tasks;
using Task_1.Models;

namespace Task_1.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}