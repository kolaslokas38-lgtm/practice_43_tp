namespace Task;

public class VolumeManager
{
    private VolumeControl volumeControl;
    private Display display;
    private SpeakerSystem speakers;

    public VolumeManager()
    {
        volumeControl = new VolumeControl();
        display = new Display();
        speakers = new SpeakerSystem();

        Subscribe();
    }

    private void Subscribe()
    {
        volumeControl.VolumeChanged += display.ShowVolume;
        volumeControl.VolumeChanged += speakers.AdjustVolume;
    }

    public void ChangeVolume(int newVolume)
    {
        volumeControl.Volume = newVolume;
    }

    public void Unsubscribe()
    {
        volumeControl.VolumeChanged -= display.ShowVolume;
        volumeControl.VolumeChanged -= speakers.AdjustVolume;
    }
}