using System;

namespace Task;

public class LoyalCustomer : ICustomer
{
    private string name;

    public LoyalCustomer(string name)
    {
        this.name = name;
    }

    public string GetName()
    {
        return name;
    }

    public void Update(string promotion)
    {
        Console.WriteLine($"  [{name}] Узнал первым! {promotion}");
    }
}