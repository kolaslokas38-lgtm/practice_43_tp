using System;
using System.Collections;
using Task1;

namespace Task;

public class ShoppingCart
{
    private Hashtable cart = new Hashtable();

    public void AddProduct(Product product)
    {
        if (cart.ContainsKey(product.Id))
        {
            Console.WriteLine($"Товар {product.Name} уже в корзине");
        }
        else
        {
            cart[product.Id] = product;
            Console.WriteLine($"Добавлен: {product.Name}");
        }
    }

    public void RemoveProduct(int id)
    {
        if (cart.ContainsKey(id))
        {
            Product product = (Product)cart[id];
            cart.Remove(id);
            Console.WriteLine($"Удален: {product.Name}");
        }
        else
        {
            Console.WriteLine($"Товар с id {id} не найден");
        }
    }

    public Product FindProduct(int id)
    {
        return cart.ContainsKey(id) ? (Product)cart[id] : null;
    }

    public void ShowAll()
    {
        if (cart.Count == 0)
        {
            Console.WriteLine("Корзина пуста");
            return;
        }

        Console.WriteLine("Корзина:");

        foreach (DictionaryEntry entry in cart)
        {
            Product p = (Product)entry.Value;
            Console.WriteLine($"  {p.Id}. {p.Name} - {p.Price:C}");
        }

        Console.WriteLine($"Итого: {GetTotal():C}");
    }

    public decimal GetTotal()
    {
        decimal total = 0;

        foreach (DictionaryEntry entry in cart)
        {
            Product p = (Product)entry.Value;
            total += p.Price;
        }

        return total;
    }

    public void Clear()
    {
        cart.Clear();
        Console.WriteLine("Корзина очищена");
    }
}