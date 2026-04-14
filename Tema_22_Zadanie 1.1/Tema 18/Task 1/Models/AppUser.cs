using System.ComponentModel.DataAnnotations;

namespace Task_1.Models
{
    public class AppUser
    {
        public int Id { get; set; }

        [MaxLength(64)]
        public string Username { get; set; } = "";

        public byte[] PasswordHash { get; set; } = [];
        public byte[] PasswordSalt { get; set; } = [];

        public UserRole Role { get; set; } = UserRole.Student;

        public int? StudentId { get; set; }
        public Student? Student { get; set; }
    }
}

