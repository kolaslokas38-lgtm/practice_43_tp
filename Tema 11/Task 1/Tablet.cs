using System;

namespace Task;

public class Tablet : IElectronicDevice
{
    public void TurnOn()
    {
        Console.WriteLine("Планшет: загрузка...");
    }

    public string GetName()
    {
        return "Планшет";
    }
}