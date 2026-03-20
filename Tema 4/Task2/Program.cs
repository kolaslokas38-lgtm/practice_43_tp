using System;

namespace Task;

public class Program
{
    public static void SortDec3(ref double a, ref double b, ref double c)
    {

        if (a < b)
        {
            double temp = a;
            a = b;
            b = temp;
        }

        if (a < c)
        {
            double temp = a;
            a = c;
            c = temp;
        }

        if (b < c)
        {
            double temp = b;
            b = c;
            c = temp;
        }
    }

    public static void Main()
    {
        Console.WriteLine("Введите a1:");
        double a1 = double.Parse(Console.ReadLine());
        Console.WriteLine("Введите b1:");
        double b1 = double.Parse(Console.ReadLine());
        Console.WriteLine("Введите c1:");
        double c1 = double.Parse(Console.ReadLine());

        Console.WriteLine("Введите a2:");
        double a2 = double.Parse(Console.ReadLine());
        Console.WriteLine("Введите b2:");
        double b2 = double.Parse(Console.ReadLine());
        Console.WriteLine("Введите c2:");
        double c2 = double.Parse(Console.ReadLine());   

        Console.WriteLine("Первый набор:");
        Console.WriteLine($"До сортировки: A = {a1}, B = {b1}, C = {c1}");

        SortDec3(ref a1, ref b1, ref c1);

        Console.WriteLine($"После сортировки: A = {a1}, B = {b1}, C = {c1}");
        Console.WriteLine();

        Console.WriteLine("Второй набор:");
        Console.WriteLine($"До сортировки: A = {a2}, B = {b2}, C = {c2}");

        SortDec3(ref a2, ref b2, ref c2);

        Console.WriteLine($"После сортировки: A = {a2}, B = {b2}, C = {c2}");
    }
}