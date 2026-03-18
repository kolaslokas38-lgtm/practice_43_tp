using System;

namespace Tema2
{
    public class Program
    {
        public static void Main()
        {
            double[] numbers = { 1.5, 2.8, 3.5, 0.5, 4.1, 1.9, 2.2, 3.0, 0.9, 2.5 };

            Console.WriteLine("Элементы массива, удовлетворяющие условию 0 < xi < 3.2:");

            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] > 0 && numbers[i] < 3.2)
                {
                    Console.WriteLine($"Индекс {i}: значение {numbers[i]}");
                }
            }
        }
    }
}