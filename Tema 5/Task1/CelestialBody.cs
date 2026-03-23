using System;

namespace Task;

public abstract class CelestialBody
{
    public string Name { get; set; }
    public double Mass { get; set; }

    protected CelestialBody(string name, double mass)
    {
        Name = name;
        Mass = mass;
    }

    public abstract string GetType();

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Название планеты: {Name}");
        Console.WriteLine($"Масса планеты: {Mass} кг");
        Console.WriteLine($"Тип планеты: {GetType()}");
    }
}