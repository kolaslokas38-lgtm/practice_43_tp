using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JournalApp.Models;

namespace JournalApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<StudentGrade> _students = new();

        public ObservableCollection<StudentGrade> Students
        {
            get => _students;
            set { _students = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            Students.Add(new StudentGrade { Name = "Швед Руслан", Grade = 5, IsPresent = true });
            Students.Add(new StudentGrade { Name = "Егор Петров", Grade = 4, IsPresent = true });
            Students.Add(new StudentGrade { Name = "Мирослав Седеневский", Grade = 3, IsPresent = false });
            Students.Add(new StudentGrade { Name = "Богдан Макарчук", Grade = 2, IsPresent = true });
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}