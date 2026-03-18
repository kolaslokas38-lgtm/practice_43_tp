using System;

class Program
{
    static void Main()
    {
        int[][] numbers = new int[5][];

        numbers[0] = [1, 2, 3, 4];
        numbers[1] = [5, 6, 7, 8];
        numbers[2] = [1, 2, 3, 4];
        numbers[3] = [9, 10, 11, 12];
        numbers[4] = [13, 14, 15, 16];

        Console.WriteLine("Массив:");

        for (int i = 0; i < numbers.Length; i++)
        {
            for (int j = 0; j < numbers[i].Length; j++)
            {
                Console.Write($"{numbers[i][j]} ");
            }

            Console.WriteLine();
        }

        bool hasDuplicates = false;

        for (int i = 0; i < numbers.Length; i++)
        {
            for (int k = i + 1; k < numbers.Length; k++)
            {
                if (numbers[i].Length != numbers[k].Length)
                {
                    continue;
                }

                bool isEqual = true;

                for (int j = 0; j < numbers[i].Length; j++)
                {
                    if (numbers[i][j] != numbers[k][j])
                    {
                        isEqual = false;

                        break;
                    }
                }

                if (isEqual)
                {
                    hasDuplicates = true;

                    Console.WriteLine($"Ряды {i} и {k} одинаковые.");
                }
            }
        }

        if (!hasDuplicates)
        {
            Console.WriteLine("Одинаковых рядов нет.");
        }
    }
}