using System;

namespace Task;

public class NetworkAdapter : IWiFiConnection, IEthernetConnection
{
    void IWiFiConnection.Connect()
    {
        Console.WriteLine("Подключение через Wi-Fi");
    }

    void IEthernetConnection.Connect()
    {
        Console.WriteLine("Подключение через Ethernet");
    }
}