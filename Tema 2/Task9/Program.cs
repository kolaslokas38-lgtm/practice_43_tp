using System;
using System.Text;

class Program
{
    static void Main()
    {
        StringBuilder sb = new StringBuilder("Структуры данных. Алгоритмы обработки структур данных");

        Console.WriteLine($"Исходная строка: {sb}");

        int length = sb.Length;

        for (int i = 0; i < length / 2; i++)
        {
            int j = length - 1 - i;

            char temp = sb[i];
            sb[i] = sb[j];
            sb[j] = temp;
        }

        Console.WriteLine($"Инвертированная: {sb}");
    }
}