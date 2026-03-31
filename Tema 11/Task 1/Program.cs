using System;

namespace Task;

public class Program
{
    public static void Main()
    {
        ElectronicDeviceFactory[] factories =
        [
            new LaptopFactory(),
            new TabletFactory(),
            new SmartphoneFactory()
        ];

        foreach (var factory in factories)
        {
            factory.TurnOnDevice();
        }

        Console.WriteLine("Создание устройств через фабрики:");

        ElectronicDeviceFactory laptopFactory = new LaptopFactory();
        IElectronicDevice laptop = laptopFactory.CreateDevice();

        ElectronicDeviceFactory tabletFactory = new TabletFactory();
        IElectronicDevice tablet = tabletFactory.CreateDevice();

        ElectronicDeviceFactory phoneFactory = new SmartphoneFactory();
        IElectronicDevice phone = phoneFactory.CreateDevice();

        laptop.TurnOn();
        tablet.TurnOn();
        phone.TurnOn();
    }
}