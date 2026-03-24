using System;

namespace Task;

public class VolumeEvent : EventArgs
{
    public int OldVolume { get; set; }
    public int NewVolume { get; set; }

    public VolumeEvent(int oldVolume, int newVolume)
    {
        OldVolume = oldVolume;
        NewVolume = newVolume;
    }
}