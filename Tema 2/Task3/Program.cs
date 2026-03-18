using System;

class Program
{
    static void Main()
    {
        Console.Write("N = ");
        int n = int.Parse(Console.ReadLine()!);

        Console.Write("a = ");
        int a = int.Parse(Console.ReadLine()!);

        Console.Write("b = ");
        int b = int.Parse(Console.ReadLine()!);

        int[,] m = new int[n, n];
        Random r = new Random();

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                m[i, j] = r.Next(a, b + 1);
                Console.Write($"{m[i, j],4} ");
            }

            Console.WriteLine();
        }

        int sum = 0;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (m[i, j] < 0)
                {
                    sum += m[i, j] * m[i, j];
                }
            }
        }

        Console.WriteLine($"Сумма квадратов: {sum}");

        Console.WriteLine("Минимумы по строкам:");

        for (int i = 0; i < n; i++)
        {
            int min = m[i, 0];

            for (int j = 1; j < n; j++)
            {
                if (m[i, j] < min)
                {
                    min = m[i, j];
                }
            }

            Console.WriteLine($"Строка {i}: {min}");
        }
    }
}