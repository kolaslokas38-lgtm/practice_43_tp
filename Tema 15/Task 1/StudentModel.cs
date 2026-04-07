using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JournalApp.Models
{
    public class StudentModel : INotifyPropertyChanged
    {
        private int _id;
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _group = string.Empty;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChanged(); }
        }

        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(); }
        }

        public string FullName => $"{LastName} {FirstName}";

        public string Group
        {
            get => _group;
            set { _group = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}