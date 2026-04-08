namespace Task.Models
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Message { get; set; } = "";
        public string Date { get; set; } = "";
        public string FromUser { get; set; } = "";
        public string ToRole { get; set; } = "";
    }
}