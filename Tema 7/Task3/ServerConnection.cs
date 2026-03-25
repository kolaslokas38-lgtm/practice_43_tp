using System;

namespace Task;

public class ServerConnection
{
    private const int MaxConnections = 10;

    public void Connect(int activeConnections)
    {
        if (activeConnections >= MaxConnections)
        {
            throw new TooManyConnectionsException(
                $"Превышен лимит подключений. Максимум: {MaxConnections}, " +
                $"текущее: {activeConnections}");
        }

        Console.WriteLine($"Подключение установлено. " +
            $"Активных подключений: {activeConnections + 1}");
    }
}