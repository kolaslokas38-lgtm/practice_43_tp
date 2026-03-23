using System;

namespace Task;

public class Program
{
    public static void Main()
    {
        Medicine[] medicines =
        [
            new Antibiotic("Магнерот", 250m, 500),
            new CoughSyrup("Гербион", 180m, 15),
            new Antibiotic("Азитромицин", 320m, 250),
            new CoughSyrup("Коделак", 220m, 10),
            new Antibiotic("Амоксициллин", 290m, 400)
        ];

        Console.WriteLine("Все лекарства:");

        for (int i = 0; i < medicines.Length; i++)
        {
            Console.WriteLine($"  {medicines[i].Name} - {medicines[i].GetType()}");
        }

        Console.WriteLine("Найденные сиропы:");

        for (int i = 0; i < medicines.Length; i++)
        {
            if (medicines[i] is ILiquidMedicine syrup)
            {
                Console.WriteLine($"  {medicines[i].Name} ({((CoughSyrup)medicines[i]).VolumeMl} мл)");
                syrup.Drink();
            }
        }
    }
}