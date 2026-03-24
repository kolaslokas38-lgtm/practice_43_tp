using System;

namespace Task;

public class VolumeControl
{
    private int volume;

    public event EventHandler<VolumeEvent>? VolumeChanged;

    public int Volume
    {
        get => volume;
        set
        {
            if (volume != value)
            {
                int oldVolume = volume;
                volume = value;

                OnVolumeChanged(new VolumeEvent(oldVolume, volume));
            }
        }
    }

    protected virtual void OnVolumeChanged(VolumeEvent e)
    {
        VolumeChanged?.Invoke(this, e);
    }
}