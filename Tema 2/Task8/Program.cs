using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string text = "кот собака кот птица рыба собака дом";

        Console.WriteLine($"Строка: {text}");

        string[] words = text.Split(' ');

        Dictionary<string, int> counts = new Dictionary<string, int>();

        for (int i = 0; i < words.Length; i++)
        {
            if (counts.ContainsKey(words[i]))
            {
                counts[words[i]]++;
            }
            else
            {
                counts[words[i]] = 1;
            }
        }

        Console.WriteLine("Слова, встречающиеся только один раз:");

        bool found = false;

        for (int i = 0; i < words.Length; i++)
        {
            if (counts[words[i]] == 1)
            {
                Console.WriteLine($"- {words[i]}");

                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Таких слов нет.");
        }
    }
}