namespace Task;

public abstract class ElectronicDeviceFactory
{
    public abstract IElectronicDevice CreateDevice();

    public void TurnOnDevice()
    {
        IElectronicDevice device = CreateDevice();
        Console.Write($"{device.GetName()}: ");
        device.TurnOn();
    }
}