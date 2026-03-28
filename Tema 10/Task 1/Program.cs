using System;

namespace Task;

public class Program
{
    public static void Main()
    {
        DiscountManager manager = DiscountManager.GetInstance();
        DiscountManager sameManager = DiscountManager.GetInstance();

        Console.WriteLine($"Один объект: {manager == sameManager}\n");

        manager.SetDiscount("Ноутбук", 15);
        manager.SetDiscount("Смартфон", 10);
        manager.SetDiscount("Наушники", 25);

        manager.ShowAllDiscounts();

        Console.WriteLine($"\nСкидка на Ноутбук: {manager.GetDiscount("Ноутбук")}%");
        Console.WriteLine($"Скидка на Планшет: {manager.GetDiscount("Планшет")}%");
    }
}