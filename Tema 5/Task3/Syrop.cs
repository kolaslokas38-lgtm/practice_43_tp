using System;

namespace Task;

public sealed class CoughSyrup : Medicine, ILiquidMedicine
{
    public double VolumeMl { get; set; }

    public CoughSyrup(string name, decimal price, double volumeMl)
        : base(name, price)
    {
        VolumeMl = volumeMl;
    }

    public override string GetType()
    {
        return "Сироп от кашля";
    }

    public void Drink()
    {
        Console.WriteLine($"Выпить {VolumeMl} мл {Name}");
    }
}