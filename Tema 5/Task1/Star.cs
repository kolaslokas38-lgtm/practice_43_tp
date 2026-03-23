using System;

namespace Task;

public sealed class Star : CelestialBody
{
    public double Temperature { get; set; }

    public Star(string name, double mass, double temperature)
        : base(name, mass)
    {
        Temperature = temperature;
    }

    public override string GetType()
    {
        return "Звезда";
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Температура: {Temperature} K");
    }
}