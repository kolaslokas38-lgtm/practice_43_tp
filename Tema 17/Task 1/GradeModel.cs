namespace JournalApp.Models
{
    public class GradeModel
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int Value { get; set; }
        public string Comment { get; set; } = "";
        public string Date { get; set; } = "";
        public string CourseName { get; set; } = "";
    }
}