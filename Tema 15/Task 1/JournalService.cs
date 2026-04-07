using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JournalApp.Models;

namespace JournalApp.Services
{
    public class JournalService
    {
        private List<StudentModel> _students = new();
        private List<CourseModel> _courses = new();
        private List<GradeModel> _grades = new();

        public async Task<List<StudentModel>> LoadStudentsAsync()
        {
            await Task.Delay(3000);

            _students = new List<StudentModel>
            {
                new StudentModel { Id = 1, FirstName = "Анна", LastName = "Кузнецова", Group = "Группа 1" },
                new StudentModel { Id = 2, FirstName = "Иван", LastName = "Петров", Group = "Группа 1" },
                new StudentModel { Id = 3, FirstName = "Мария", LastName = "Сидорова", Group = "Группа 2" },
                new StudentModel { Id = 4, FirstName = "Дмитрий", LastName = "Иванов", Group = "Группа 2" },
                new StudentModel { Id = 5, FirstName = "Елена", LastName = "Смирнова", Group = "Группа 1" }
            };

            return _students;
        }

        public async Task<List<CourseModel>> LoadCoursesAsync()
        {
            await Task.Delay(500);

            _courses = new List<CourseModel>
            {
                new CourseModel { Id = 1, Name = "Математика", Credits = 5 },
                new CourseModel { Id = 2, Name = "Программирование", Credits = 6 },
                new CourseModel { Id = 3, Name = "Физика", Credits = 4 },
                new CourseModel { Id = 4, Name = "Английский язык", Credits = 3 }
            };

            return _courses;
        }

        public async Task<List<GradeModel>> LoadGradesAsync()
        {
            await Task.Delay(500);

            _grades = new List<GradeModel>
            {
                new GradeModel { Id = 1, StudentId = 1, CourseId = 1, Value = 5, Date = DateTime.Now.AddDays(-10) },
                new GradeModel { Id = 2, StudentId = 1, CourseId = 2, Value = 4, Date = DateTime.Now.AddDays(-8) },
                new GradeModel { Id = 3, StudentId = 2, CourseId = 1, Value = 4, Date = DateTime.Now.AddDays(-9) },
                new GradeModel { Id = 4, StudentId = 2, CourseId = 2, Value = 5, Date = DateTime.Now.AddDays(-7) },
                new GradeModel { Id = 5, StudentId = 3, CourseId = 1, Value = 3, Date = DateTime.Now.AddDays(-6) },
                new GradeModel { Id = 6, StudentId = 3, CourseId = 2, Value = 4, Date = DateTime.Now.AddDays(-5) }
            };

            return _grades;
        }

        public async Task<bool> SaveGradeAsync(GradeModel grade)
        {
            await Task.Delay(500);

            var existing = _grades.FirstOrDefault(g => g.Id == grade.Id);
            if (existing != null)
            {
                existing.Value = grade.Value;
                existing.Date = DateTime.Now;
            }
            else
            {
                grade.Id = _grades.Max(g => g.Id) + 1;
                grade.Date = DateTime.Now;
                _grades.Add(grade);
            }

            return true;
        }

        public double GetAverageGradeForStudent(int studentId)
        {
            var studentGrades = _grades.Where(g => g.StudentId == studentId);
            if (!studentGrades.Any()) return 0;
            return studentGrades.Average(g => g.Value);
        }

        public List<GradeModel> GetGradesForStudent(int studentId)
        {
            return _grades.Where(g => g.StudentId == studentId).ToList();
        }
    }
}