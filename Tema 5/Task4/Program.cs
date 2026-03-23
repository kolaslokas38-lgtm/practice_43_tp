using System;

namespace Task;

public class Program
{
    public static void Main()
    {
        NetworkAdapter adapter = new NetworkAdapter();

        IWiFiConnection wifi = adapter;
        IEthernetConnection ethernet = adapter;

        Console.WriteLine("Явная реализация интерфейсов:");

        wifi.Connect();
        ethernet.Connect();

    }
}