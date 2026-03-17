using System;

public class Program
{
    public static void Main()
    {
        double x = 5.0;

        double y;

        if (x > 6.7)
        {
            y = 4 - Math.Exp(4 * x);
        }
        else
        {
            y = Math.Log(3.5 + x);
        }

        Console.WriteLine($"x = {x}");
        Console.WriteLine($"y = {y:F4}");
    }
}