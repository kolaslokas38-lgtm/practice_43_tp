using System;

namespace Task;

public class RegularCustomer : ICustomer
{
    private string name;

    public RegularCustomer(string name)
    {
        this.name = name;
    }

    public string GetName()
    {
        return name;
    }

    public void Update(string promotion)
    {
        Console.WriteLine($"  [{name}] (обычный): Узнал об акции: {promotion}");
    }
}