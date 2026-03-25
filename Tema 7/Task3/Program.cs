using System;

namespace Task;

public class Program
{
    public static void Main()
    {
        ServerConnection server = new ServerConnection();

        int[] connections = [5, 8, 10, 12];

        for (int i = 0; i < connections.Length; i++)
        {
            Console.WriteLine($"\nПопытка {i + 1}: {connections[i]} подключений");

            try
            {
                server.Connect(connections[i]);
            }
            catch (TooManyConnectionsException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}