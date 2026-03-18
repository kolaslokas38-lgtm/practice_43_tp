using System;

class Program
{
    static void Main()
    {
        string text = "Структуры данных. Алгоритмы обработки структур данных";

        Console.WriteLine($"Строка: {text}");

        int count = 0;

        for (int i = 0; i < text.Length - 1; i++)
        {
            if (text[i] == text[i + 1])
            {
                count++;

                Console.WriteLine($"Найдена пара: '{text[i]}{text[i + 1]}' на позиции {i}");
            }
        }

        Console.WriteLine($"Всего пар одинаковых подряд идущих символов: {count}");
    }
}