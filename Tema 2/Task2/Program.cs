using System;

namespace Tema2
{
    public class Program
    {
        public static void Main()
        {
            double[] numbers =
            [
                3.5, 7.2, 1.8, 9.4, 2.1,
                5.6, 4.3, 8.7, 6.2, 0.9,
                4.8, 2.5, 7.1, 3.9, 6.4,
                1.2, 9.8, 5.3, 0.7, 8.5
            ];

            Console.WriteLine("Исходный массив:");
            PrintArray(numbers);

            for (int i = 0; i < 10; i++)
            {
                if (numbers[i] < numbers[i + 10])
                {
                    (numbers[i], numbers[i + 10]) = (numbers[i + 10], numbers[i]);
                }
            }

            Console.WriteLine("Преобразованный массив:");
            PrintArray(numbers);

            Array.Sort(numbers);
            Console.WriteLine("Отсортированный массив:");
            PrintArray(numbers);

            Console.Write("Введите k: ");
            double k = double.Parse(Console.ReadLine()!);

            int index = Array.BinarySearch(numbers, k);

            if (index >= 0)
            {
                Console.WriteLine($"Число {k} найдено на индексе {index}.");
            }
            else
            {
                Console.WriteLine($"Число {k} не найдено. " +
                    $"Индекс вставки: {~index}.");
            }
        }

        private static void PrintArray(double[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.WriteLine($"a[{i}] = {numbers[i]:F2}");
            }
        }
    }
}