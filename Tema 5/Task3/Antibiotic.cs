using System;

namespace Task;

public sealed class Antibiotic : Medicine, IPill
{
    public int DosageMg { get; set; }

    public Antibiotic(string name, decimal price, int dosageMg)
        : base(name, price)
    {
        DosageMg = dosageMg;
    }

    public override string GetType()
    {
        return "Антибиотик (таблетки)";
    }

    public void Take()
    {
        Console.WriteLine($"Принять {DosageMg} мг {Name}");
    }
}