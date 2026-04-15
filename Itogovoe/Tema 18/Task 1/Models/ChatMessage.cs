using System.ComponentModel.DataAnnotations;

namespace Task_1.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        [MaxLength(500)]
        public string Text { get; set; } = string.Empty;

        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public AppUser? Sender { get; set; }
        public AppUser? Receiver { get; set; }
    }
}
