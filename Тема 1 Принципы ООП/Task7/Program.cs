using System;

public class Program
{
    public static void Main()
    {
        int a = 10;
        int b = 50;
        int x = 2;
        int y = 6;

        Console.WriteLine($"Диапазон: от {a} до {b}");
        Console.WriteLine($"Ищем числа, оканчивающиеся на {x} или {y}");
        Console.WriteLine();

        Console.WriteLine("Способ 1 (for):");
        for (int i = b; i >= a; i--)
        {
            if ((i % 2 == 0) && ((i % 10 == x) || (i % 10 == y)))
            {
                Console.Write($"{i} ");
            }
        }
        Console.WriteLine();
        Console.WriteLine();

        Console.WriteLine("Способ 2 (while):");
        int j = b;
        while (j >= a)
        {
            if ((j % 2 == 0) && ((j % 10 == x) || (j % 10 == y)))
            {
                Console.Write($"{j} ");
            }
            j--;
        }
        Console.WriteLine();
        Console.WriteLine();

        Console.WriteLine("Способ 3 (do while):");
        int k = b;
        do
        {
            if ((k % 2 == 0) && ((k % 10 == x) || (k % 10 == y)))
            {
                Console.Write($"{k} ");
            }
            k--;
        }
        while (k >= a);
        Console.WriteLine();
    }
}