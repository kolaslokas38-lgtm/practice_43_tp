namespace Task_1.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Course { get; set; } = "";
        public int Grade { get; set; }
        public virtual Student? Student { get; set; }
    }
}