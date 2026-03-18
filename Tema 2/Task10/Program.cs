using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string text = "abc123def456ghi789jkl";

        Console.WriteLine($"Исходная строка: {text}");

        string result = Regex.Replace(text, @"\d", "");

        Console.WriteLine($"Результат:       {result}");
    }
}