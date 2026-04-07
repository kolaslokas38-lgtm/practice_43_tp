using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JournalApp.Models
{
    public class GradeModel : INotifyPropertyChanged
    {
        private int _id;
        private int _studentId;
        private int _courseId;
        private int _value;
        private DateTime _date;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        public int StudentId
        {
            get => _studentId;
            set { _studentId = value; OnPropertyChanged(); }
        }

        public int CourseId
        {
            get => _courseId;
            set { _courseId = value; OnPropertyChanged(); }
        }

        public int Value
        {
            get => _value;
            set { _value = value; OnPropertyChanged(); }
        }

        public DateTime Date
        {
            get => _date;
            set { _date = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}