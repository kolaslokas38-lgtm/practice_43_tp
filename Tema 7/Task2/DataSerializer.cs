using System;

namespace Task;

public class DataSerializer
{
    private Serializer serializer = new Serializer();

    public void SerializeData(object obj)
    {
        try
        {
            serializer.Serialize(obj);
        }
        catch (InvalidOperationException ex)
        {
            throw new SerializationOperationException($"Ошибка сериализации данных", ex);
        }
    }
}