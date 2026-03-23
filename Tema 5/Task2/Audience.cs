using System;

namespace Task;

public class Audience
{
    public int PeopleCount { get; set; }

    public Audience(int count)
    {
        PeopleCount = count;
    }

    public void Watch()
    {
        Console.WriteLine($"{PeopleCount} зрителей смотрят спектакль");
    }

    public void Clap()
    {
        Console.WriteLine("Зрители аплодируют!");
    }
}