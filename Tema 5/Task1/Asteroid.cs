using System;

namespace Task;

public sealed class Asteroid : CelestialBody
{
    public string Composition { get; set; }

    public Asteroid(string name, double mass, string composition)
        : base(name, mass)
    {
        Composition = composition;
    }

    public override string GetType()
    {
        return "Астероид";
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Состав: {Composition}");
    }
}