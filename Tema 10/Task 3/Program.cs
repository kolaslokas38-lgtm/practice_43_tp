using System;

namespace Task;

public class Program
{
    public static void Main()
    {
        PromotionManager manager = new PromotionManager();

        ICustomer ivan = new LoyalCustomer("Мирослав");
        ICustomer maria = new LoyalCustomer("Руслан");
        ICustomer petr = new RegularCustomer("Рома");
        ICustomer anna = new RegularCustomer("Богдан");

        manager.Subscribe(ivan);
        manager.Subscribe(maria);
        manager.Subscribe(petr);
        manager.Subscribe(anna);

        manager.AddPromotion("Скидка 20% на все товары!");

        Console.WriteLine();
        manager.Unsubscribe(petr);

        manager.AddPromotion("Бесплатная доставка при заказе от 50BYN");

        Console.WriteLine();
        manager.AddPromotion("Товары дня: скидка 50%");
    }
}