using System;

namespace Task;

public class NonSerializableClass
{
    public string Name { get; set; }
}

[Serializable]
public class SerializableClass
{
    public string Name { get; set; }
}

public class Program
{
    public static void Main()
    {
        DataSerializer dataSerializer = new DataSerializer();

        NonSerializableClass nonSerializable = new NonSerializableClass
        {
            Name = "Test"
        };

        SerializableClass serializable = new SerializableClass
        {
            Name = "Test"
        };

        try
        {
            Console.WriteLine("Попытка 1:");
            dataSerializer.SerializeData(nonSerializable);
        }
        catch (SerializationOperationException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            Console.WriteLine($"Внутреннее исключение: {ex.InnerException?.Message}");
            Console.WriteLine($"Стек вызовов: {ex.StackTrace}");
        }

        Console.WriteLine();

        try
        {
            Console.WriteLine("Попытка 2:");
            dataSerializer.SerializeData(serializable);
        }
        catch (SerializationOperationException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}