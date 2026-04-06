using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JournalApp.Models
{
    public class StudentGrade : INotifyPropertyChanged
    {
        private string _name = string.Empty;
        private int _grade;
        private bool _isPresent;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }
        public int Grade
        {
            get => _grade;
            set
            {
                _grade = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AverageScore)); 
            }
        }

        public bool IsPresent
        {
            get => _isPresent;
            set
            {
                _isPresent = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AverageScore)); 
            }
        }

        public double AverageScore
        {
            get
            {
                if (!IsPresent) return 0;
                return Grade;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}