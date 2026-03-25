using System;

namespace Task;

public class Serializer
{
    public void Serialize(object obj)
    {
        if (!obj.GetType().IsSerializable)
        {
            throw new InvalidOperationException(
                $"Объект типа {obj.GetType().Name} не поддерживает сериализацию");
        }

        Console.WriteLine($"Объект {obj} сериализован");
    }
}