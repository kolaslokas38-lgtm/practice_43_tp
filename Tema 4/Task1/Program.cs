using System;

namespace Task;

public static class CircleCalculator
{
    public static double GetArea(double radius)
    {
        if (radius < 0)
        {
            return 0;
        }

        double area = Math.PI * radius * radius;

        return area;
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Введите радиус r:");
        double r = double.Parse(Console.ReadLine());

        double area = CircleCalculator.GetArea(r);

        Console.WriteLine($"Радиус: {r}");
        Console.WriteLine($"Площадь круга: {area:F4}");
    }
}