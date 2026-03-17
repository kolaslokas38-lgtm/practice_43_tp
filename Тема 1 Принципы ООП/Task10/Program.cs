using System;

class Program
{
    public static void Main()
    {
        for (int i = 100; i < 1000; i++)
        {
            if (i == (i * i) % 1000)
            {
                Console.WriteLine($"{i}^2 = {i * i}");
            }
        }
    }
}