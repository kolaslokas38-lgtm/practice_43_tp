using System.Collections.Generic;

namespace JournalApp.Models
{
    public class CourseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public List<EnrollmentModel> Enrollments { get; set; } = new();
    }
}