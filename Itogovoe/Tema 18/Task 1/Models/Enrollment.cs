namespace Task_1.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int Grade { get; set; }
        public virtual Student? Student { get; set; }
        public virtual Course? Course { get; set; }
    }
}