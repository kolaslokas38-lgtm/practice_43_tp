using System;

namespace Task;

public class Program
{
    public static int FindMax(int a, int b)
    {
        return a > b ? a : b;
    }

    public static double FindMax(double a, double b)
    {
        return a > b ? a : b;
    }

    public static void Main()
    {
        Console.WriteLine(FindMax(3, 5));
        Console.WriteLine(FindMax(3.1, 2.9));
    }
}