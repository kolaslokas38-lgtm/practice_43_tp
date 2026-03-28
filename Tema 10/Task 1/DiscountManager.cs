using System;
using System.Collections.Generic;

namespace Task;

public class DiscountManager
{
    private static DiscountManager? instance;
    private Dictionary<string, double> discounts = new Dictionary<string, double>();

    private DiscountManager() { }

    public static DiscountManager GetInstance()
    {
        if (instance == null)
        {
            instance = new DiscountManager();
        }

        return instance;
    }

    public void SetDiscount(string product, double percent)
    {
        if (percent < 0 || percent > 100)
        {
            Console.WriteLine($"Ошибка: скидка {percent}% вне диапазона 0-100");
            return;
        }

        discounts[product] = percent;
        Console.WriteLine($"Скидка {percent}% установлена для {product}");
    }

    public double GetDiscount(string product)
    {
        if (discounts.ContainsKey(product))
        {
            return discounts[product];
        }

        return 0;
    }

    public void ShowAllDiscounts()
    {
        if (discounts.Count == 0)
        {
            Console.WriteLine("Скидок нет");
            return;
        }

        Console.WriteLine("\nТекущие скидки:");

        foreach (var item in discounts)
        {
            Console.WriteLine($"  {item.Key}: {item.Value}%");
        }
    }
}