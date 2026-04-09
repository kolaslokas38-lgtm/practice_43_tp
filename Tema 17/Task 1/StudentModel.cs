namespace JournalApp.Models
{
    public class StudentModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public double AverageGrade { get; set; }
        public bool HasLowGrades { get; set; }
    }
}