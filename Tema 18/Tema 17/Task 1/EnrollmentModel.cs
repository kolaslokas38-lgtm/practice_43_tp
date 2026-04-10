using System.Collections.Generic;

namespace JournalApp.Models
{
    public class EnrollmentModel
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public StudentModel? Student { get; set; }

        public int CourseId { get; set; }
        public CourseModel? Course { get; set; }

        public List<GradeModel> Grades { get; set; } = new();
        public string StudentName => Student?.Name ?? "";
        public string CourseName => Course?.Name ?? "";
    }
}

