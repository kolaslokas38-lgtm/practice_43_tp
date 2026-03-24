using System;

namespace Task4;

public class Program
{
    public static void Main()
    {
        VolumeManager manager = new VolumeManager();

        Console.WriteLine("Изменение громкости");
        manager.ChangeVolume(30);
        manager.ChangeVolume(50);
        manager.ChangeVolume(100);
    }
}