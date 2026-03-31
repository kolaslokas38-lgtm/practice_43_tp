namespace Task;

public class LaptopFactory : ElectronicDeviceFactory
{
    public override IElectronicDevice CreateDevice()
    {
        return new Laptop();
    }
}