using System;

class Program
{
    public static void Main()
    {
        int m = 15;
        double a = Math.PI / 4;
        double b = Math.PI / 2;
        double h = (b - a) / m;
        double y;

        for (double i = a; i <= b; i += h)
        {
            y = 2 - Math.Sin(i);
            Console.WriteLine($"y = {y:N4}, x = {i:N4}");
        }
    }
}