using System;

namespace Task;

public class Laptop : IElectronicDevice
{
    public void TurnOn()
    {
        Console.WriteLine("Ноутбук: загрузка...");
    }

    public string GetName()
    {
        return "Ноутбук";
    }
}