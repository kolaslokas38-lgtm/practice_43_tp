using System;

namespace Task;

public sealed class Planet : CelestialBody
{
    public int MoonCount { get; set; }

    public Planet(string name, double mass, int moonCount)
        : base(name, mass)
    {
        MoonCount = moonCount;
    }

    public override string GetType()
    {
        return "Планета";
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Количество спутников: {MoonCount}");
    }
}