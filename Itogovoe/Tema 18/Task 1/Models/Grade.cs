namespace JournalApp.Models
{
    public class GradeModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = "";
        public string Course { get; set; } = "";
        public int Value { get; set; }
        public string Comment { get; set; } = "";
        public string Date { get; set; } = "";
    }
}