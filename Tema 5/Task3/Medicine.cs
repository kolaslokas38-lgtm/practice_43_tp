namespace Task;

public abstract class Medicine
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    protected Medicine(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public abstract string GetType();
}   