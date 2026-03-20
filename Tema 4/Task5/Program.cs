using System;

namespace Task;

public abstract class Plant
{
    public string Name { get; set; }
    public int Age { get; set; }

    protected Plant(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public abstract void Grow();

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Растение: {Name}, Возраст: {Age} лет");
    }
}

public sealed class Tree : Plant
{
    public double Height { get; set; }

    public Tree(string name, int age, double height)
        : base(name, age)
    {
        Height = height;
    }

    public override void Grow()
    {
        Height += 0.5;
        Console.WriteLine("Дерево растет");
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"  Высота: {Height} м");
    }
}

public sealed class Flower : Plant
{
    public string Color { get; set; }

    public Flower(string name, int age, string color)
        : base(name, age)
    {
        Color = color;
    }

    public override void Grow()
    {
        Console.WriteLine("Цветок растет");
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"  Цвет: {Color}");
    }
}

public class Program
{
    public static void Main()
    {
        Plant[] plants =
        [
            new Tree("Дуб", 10, 5.2),
            new Flower("Роза", 2, "красный"),
            new Tree("Сосна", 7, 3.8),
            new Flower("Тюльпан", 1, "желтый")
        ];

        for (int i = 0; i < plants.Length; i++)
        {
            plants[i].DisplayInfo();
            plants[i].Grow();
            Console.WriteLine();
        }
    }
}