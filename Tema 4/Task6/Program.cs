using System;

namespace Task;

public class Program
{
    public static int SecondsInMonth(int m, int y)
    {
        int[] days = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

        if (m == 2 && IsLeapYear(y))
        {
            return 29 * 24 * 3600;
        }

        return days[m - 1] * 24 * 3600;
    }

    private static bool IsLeapYear(int y)
    {
        return (y % 4 == 0 && y % 100 != 0) || (y % 400 == 0);
    }

    public static void Main()
    {
        int y = 2024;
        int m1 = 2;
        int m2 = 4;
        int m3 = 12;

        Console.WriteLine($"Год: {y}");
        Console.WriteLine($"Секунд в феврале: {SecondsInMonth(m1, y)}");
        Console.WriteLine($"Секунд в апреле: {SecondsInMonth(m2, y)}");
        Console.WriteLine($"Секунд в декабре: {SecondsInMonth(m3, y)}");
    }
}