using System;
using System.Collections.Generic;

namespace Task;

public static class ListExtensions
{
    public static void SortByLength(this List<string> list)
    {
        for (int i = 0; i < list.Count - 1; i++)
        {
            for (int j = 0; j < list.Count - 1 - i; j++)
            {
                if (list[j].Length > list[j + 1].Length)
                {
                    string temp = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = temp;
                }
            }
        }
    }

    public static void Print(this List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine($"  [{i}] {list[i]} (длина: {list[i].Length})");
        }
    }
}

public class Program
{
    public static void Main()
    {
        List<string> words =
        [
            "программирование",
            "кот",
            "C#",
            "расширяющие методы",
            "код",
            "рефакторинг"
        ];

        Console.WriteLine("Исходный список:");
        words.Print();

        words.SortByLength();

        Console.WriteLine("\nСписок после сортировки по длине:");
        words.Print();
    }
}