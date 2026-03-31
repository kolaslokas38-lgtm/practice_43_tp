namespace Task;

public class SmartphoneFactory : ElectronicDeviceFactory
{
    public override IElectronicDevice CreateDevice()
    {
        return new Smartphone();
    }
}