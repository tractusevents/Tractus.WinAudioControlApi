using NAudio.CoreAudioApi;

public class AudioDeviceModel
{
    public string ID { get; }
    public string DeviceFriendlyName { get; }
    public string FriendlyName { get; }
    public DeviceState State { get; }
    public bool Mute { get; }
    public float VolumeLevel { get; }

    public AudioDeviceModel(
        string id, 
        string deviceFriendlyName, 
        string friendlyName,
        DeviceState state, 
        bool mute, 
        float volumeLevel)
    {
        this.ID = id;
        this.DeviceFriendlyName = deviceFriendlyName;
        this.FriendlyName = friendlyName;
        this.State = state;
        this.Mute = mute;
        this.VolumeLevel = volumeLevel;
    }
}
