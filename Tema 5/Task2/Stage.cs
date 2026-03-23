using System;

namespace Task;

public class Stage
{
    public string Name { get; set; }
    public int Capacity { get; set; }

    public Stage(string name, int capacity)
    {
        Name = name;
        Capacity = capacity;
    }

    public void Prepare()
    {
        Console.WriteLine($"Сцена {Name} готова к выступлению");
    }
}