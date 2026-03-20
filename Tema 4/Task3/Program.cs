using System;

namespace Task;

public class Program
{
    public static int FindIndex(int[] array, int value)
    {
        return FindIndexRecursive(array, value, 0);
    }

    private static int FindIndexRecursive(int[] array, int value, int index)
    {
        if (index >= array.Length)
        {
            return -1;
        }

        if (array[index] == value)
        {
            return index;
        }

        return FindIndexRecursive(array, value, index + 1);
    }

    public static void Main()
    {
        int[] numbers = [5, 3, 9, 1, 7, 4];

        Console.WriteLine("Введите значение searchValue1:");
        int searchValue1 = int.Parse(Console.ReadLine());
        int index1 = FindIndex(numbers, searchValue1);

        Console.WriteLine($"Массив: [{string.Join(", ", numbers)}]");
        Console.WriteLine($"Поиск числа {searchValue1}: индекс {index1}");

        Console.WriteLine("Ввеите значение searchValue2:");
        int searchValue2 = int.Parse(Console.ReadLine());
        int index2 = FindIndex(numbers, searchValue2);

        Console.WriteLine($"Поиск числа {searchValue2}: индекс {index2}");

        Console.WriteLine("Введите дополнительное значение searchValue3");
        int searchValue3 = int.Parse(Console.ReadLine());
        int index3 = FindIndex(numbers, searchValue3);

        Console.WriteLine($"Поиск числа {searchValue3}: индекс {index3}");
    }
}