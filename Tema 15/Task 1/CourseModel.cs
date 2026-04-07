using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JournalApp.Models
{
    public class CourseModel : INotifyPropertyChanged
    {
        private int _id;
        private string _name = string.Empty;
        private int _credits;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public int Credits
        {
            get => _credits;
            set { _credits = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}