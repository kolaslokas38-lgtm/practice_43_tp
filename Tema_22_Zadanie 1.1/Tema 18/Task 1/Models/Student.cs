using JournalApp.Models;
using System.Collections.ObjectModel;

namespace Task_1.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public virtual ObservableCollection<Enrollment> Enrollments { get; set; } = new();
    }
}