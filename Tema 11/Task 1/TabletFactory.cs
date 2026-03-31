namespace Task;

public class TabletFactory : ElectronicDeviceFactory
{
    public override IElectronicDevice CreateDevice()
    {
        return new Tablet();
    }
}