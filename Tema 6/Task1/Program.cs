using System;

namespace Task1;

public delegate void VolumeControl(int percent);

public class Speaker
{
    public void SetVolume(int percent)
    {
        Console.WriteLine($"Громкость устройства: {percent}%");
    }
}

public class HeadPhones
{
    public void SetVolume(int percent)
    {
        Console.WriteLine($"Громкость наушников:{percent}%");
    }
} 
public class Program
{
    public static void Main()
    {
        Speaker speaker = new Speaker();
        HeadPhones headphone = new HeadPhones();

        VolumeControl volumeContorl;

        VolumeControl control = speaker.SetVolume;
        control(90);

        control = headphone.SetVolume;
        control(30);
    }
}