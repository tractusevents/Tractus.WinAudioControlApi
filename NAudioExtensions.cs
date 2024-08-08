using NAudio.CoreAudioApi;
public static class NAudioExtensions
{
    public static AudioDeviceModel GetModelFromMMDeviceAndDispose(this MMDevice x)
    {
        var toReturn = new AudioDeviceModel(
            x.ID,
            x.DeviceFriendlyName,
            x.FriendlyName,
            x.State,
            x.AudioEndpointVolume.Mute,
            x.AudioEndpointVolume.MasterVolumeLevelScalar);

        x.Dispose();
        return toReturn;
    }

    public static AudioDeviceVolumeModel GetVolumeModelFromMMDeviceAndDispose(this MMDevice x)
    {
        var toReturn = new AudioDeviceVolumeModel(
            x.ID,
            x.FriendlyName,
            x.AudioEndpointVolume.Mute,
            x.AudioEndpointVolume.MasterVolumeLevelScalar
        );

        x.Dispose();
        return toReturn;
    }

    private static string GetStateFromDevice(MMDevice x)
    {
        return x.State == DeviceState.Active ? "Active"
                    : x.State == DeviceState.NotPresent ? "Not Present"
                    : x.State == DeviceState.Unplugged ? "Unplugged"
                    : x.State == DeviceState.Disabled ? "Disabled"
                    : x.State == DeviceState.All ? "All"
                    : "Unknown";
    }
}

