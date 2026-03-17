using System;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Введите первое число a:");
        int a = int.Parse(Console.ReadLine());
        Console.WriteLine("Ввеидте второе число b:");
        int b = int.Parse(Console.ReadLine());

        long product = 1;

        for (int i = a; i <= b; i++)
        {
            product *= i; 
        }

        Console.WriteLine($"A = {a}, B = {b}");
        Console.WriteLine($"Произведение чисел от {a} до {b}: {product}");
    }
}