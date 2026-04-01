using System.ComponentModel;

namespace Task;

public class Student : INotifyPropertyChanged
{
    private string name;
    private string grade;
    private string comment;

    public string Name
    {
        get => name;
        set { name = value; OnPropertyChanged(nameof(Name)); }
    }

    public string Grade
    {
        get => grade;
        set { grade = value; OnPropertyChanged(nameof(Grade)); }
    }

    public string Comment
    {
        get => comment;
        set { comment = value; OnPropertyChanged(nameof(Comment)); }
    }

    public Student(string name, string grade = "", string comment = "")
    {
        Name = name;
        Grade = grade;
        Comment = comment;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}