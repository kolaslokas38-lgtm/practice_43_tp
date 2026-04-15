namespace JournalApp.Models
{
    public class GradeModel
    {
        public int Id { get; set; }

        public int EnrollmentId { get; set; }
        public EnrollmentModel? Enrollment { get; set; }

        public int Value { get; set; }
        public string Comment { get; set; } = "";
        public string Date { get; set; } = "";
        public string CourseName { get; set; } = "";
    }
}