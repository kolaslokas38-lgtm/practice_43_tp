using System;

public class Program
{
    public static void Main()
    {
        int n = 5;

        double sum = 0.0;

        for (int i = 1; i <= n; i++)
        {
            double term = 1.0 + (i / 10.0);

            int sign = 1 - 2 * (i % 2);

            sum += sign * term;
        }

        Console.WriteLine($"Result: {sum:F4}");
    }
}