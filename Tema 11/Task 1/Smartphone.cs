using System;

namespace Task;

public class Smartphone : IElectronicDevice
{
    public void TurnOn()
    {
        Console.WriteLine("Смартфон: загрузка...");
    }

    public string GetName()
    {
        return "Смартфон";
    }
}