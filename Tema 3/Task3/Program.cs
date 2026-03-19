using System;

abstract class Product
{
    public string Name { get; }
    public string Category { get; }
    public decimal Price { get; }
    public int Stock { get; }

    protected Product(string name, string category, decimal price, int stock)
    {
        Name = name;
        Category = category;
        Price = price;
        Stock = stock;
    }

    public abstract decimal GetDiscount();
}

class Electronics : Product
{
    public Electronics(string name, decimal price, int stock)
        : base(name, "Электроника", price, stock)
    {
    }

    public override decimal GetDiscount()
    {
        return Stock > 5 ? Price * 0.1m : 0;
    }
}

class Clothing : Product
{
    public string Size { get; }

    public Clothing(string name, decimal price, int stock, string size)
        : base(name, "Одежда", price, stock)
    {
        Size = size;
    }

    public override decimal GetDiscount()
    {
        return 0;
    }
}

class Store
{
    private Product[] _items;

    public Store(Product[] items)
    {
        _items = items;
    }

    public void ShowAll()
    {
        foreach (var item in _items)
        {
            Console.WriteLine($"{item.Name} | {item.Category} | {item.Price:C} | {item.Stock} шт.");
        }
    }

    public decimal TotalValue()
    {
        decimal sum = 0;

        foreach (var item in _items)
        {
            sum += item.Price * item.Stock;
        }

        return sum;
    }
}

class Program
{
    static void Main()
    {
        var products = new Product[]
        {
            new Electronics("Ноутбук", 75000m, 3),
            new Electronics("Смартфон", 45000m, 10),
            new Clothing("Футболка", 1500m, 20, "L"),
            new Clothing("Джинсы", 3500m, 5, "M")
        };

        var store = new Store(products);

        Console.WriteLine("Товары:");
        store.ShowAll();

        Console.WriteLine($"Всего: {store.TotalValue():C}");
    }
} 