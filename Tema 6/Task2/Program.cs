using System;

namespace Task2;

public delegate void ConfigSetter(string config);

public class Program
{
    public static void SetConfiguration(string value, ConfigSetter config)
    {
        Console.WriteLine("Принятие кофигурации");
        config(value);
    }

    public static void SetDataBaseConfig(string config)
    {
        Console.WriteLine($"База данных:{config}");
    }

    public static void SetCacheConfig(string config)
    {
        Console.WriteLine($"Кэш:{config}");
    }

    public static void Main()
    {
        SetConfiguration("localhost:3345", SetDataBaseConfig);
        SetConfiguration("Cache:4343",SetCacheConfig);
    }
}