using System;

namespace Task;

public class SpeakerSystem
{
    public void AdjustVolume(object? sender, VolumeEvent e)
    {
        Console.WriteLine($"Установлена громкость на колонках {e.NewVolume}%");
    }
}