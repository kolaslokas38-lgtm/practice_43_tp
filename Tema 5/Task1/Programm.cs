using System;

namespace Task;

public class Program
{
    public static void Main()
    {
        CelestialBody[] bodies =
        [
            new Planet("Земля", 5.97e24, 1),
            new Star("Солнце", 1.99e30, 5778),
            new Asteroid("Церера", 9.39e20, "камень и лед"),
            new Planet("Юпитер", 1.90e27, 79),
            new Star("Сириус", 4.02e30, 9940)
        ];

        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].DisplayInfo();
            Console.WriteLine();
        }
    }
}