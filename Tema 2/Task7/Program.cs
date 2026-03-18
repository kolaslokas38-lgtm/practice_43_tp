using System;
using System.Text;

class Program
{
    static void Main()
    {
        string text = "Структуры данных. Алгоритмы обработки структур данных";

        Console.WriteLine($"Исходная строка: {text}");

        string[] words = text.Split(' ');

        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Length > 0)
            {
                char firstChar = char.ToUpper(words[i][0]);
                string restChars = words[i].Substring(1);

                words[i] = firstChar + restChars;
            }
        }

        string result = string.Join(" ", words);

        Console.WriteLine($"Результат:       {result}");
    }
}