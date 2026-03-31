using System.Collections.ObjectModel;

namespace Task;

public class Subject
{
    public string Name { get; set; }
    public ObservableCollection<Student> Students { get; set; }

    public Subject(string name)
    {
        Name = name;
        Students = new ObservableCollection<Student>();
    }
}