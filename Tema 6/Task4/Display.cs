using System;

namespace Task;

public class Display
{
    public void ShowVolume(object? sender, VolumeEvent e)
    {
        Console.WriteLine($"Громкость изменена на динамиках: {e.OldVolume}% -> {e.NewVolume}%");
    }
}