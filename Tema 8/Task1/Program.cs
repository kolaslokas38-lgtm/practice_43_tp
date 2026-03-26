using System;
using Task;

namespace Task1;

public class Program
{
    public static void Main()
    {
        ShoppingCart cart = new ShoppingCart();

        Product p1 = new Product(1, "Компьютер", 1000000);
        Product p2 = new Product(2, "Монитор", 20000);
        Product p3 = new Product(3, "Стол", 15000);

        cart.AddProduct(p1);
        cart.AddProduct(p2);
        cart.AddProduct(p3);

        cart.ShowAll();

        Product found = cart.FindProduct(p1.Id);
        Console.WriteLine($"Поиска товара с id 1: {found.Name}");

        cart.RemoveProduct(1);
        cart.ShowAll();

        cart.Clear();
        cart.ShowAll();
    }
}