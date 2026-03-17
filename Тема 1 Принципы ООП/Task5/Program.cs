using System;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Введите сторону а:");
        double a = double.Parse(Console.ReadLine());
        Console.WriteLine("Введите сторону b:");
        double b = double.Parse(Console.ReadLine());
        Console.WriteLine("Введите сторону c:");
        double c = double.Parse(Console.ReadLine());

        bool ravnostoronniy = (a == b) && (b == c);

        Console.WriteLine($"Длины сторон: a = {a}, b = {b}, c = {c}");
        Console.WriteLine($"Треугольник равносторнний: {ravnostoronniy}");
    }
}