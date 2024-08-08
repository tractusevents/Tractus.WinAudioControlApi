using NAudio.CoreAudioApi;

public class WindowsAudioInfoController
{
    public static WindowsAudioInfoController Instance { get; }
        = new WindowsAudioInfoController();

    private WindowsAudioInfoController()
    {

    }

    public IEnumerable<AudioDeviceModel> GetOutputDevices(
        bool includeAllDevices)
    {
        return this.GetDevices(DataFlow.Render, includeAllDevices);
    }

    public IEnumerable<AudioDeviceModel> GetInputDevices(
        bool includeAllDevices)
    {
        return this.GetDevices(DataFlow.Capture, includeAllDevices);
    }

    public IDictionary<string, AudioDeviceVolumeModel> GetOutputDeviceVolumes()
    {
        return this.GetDeviceVolumes(DataFlow.Render);
    }

    public IDictionary<string, AudioDeviceVolumeModel> GetInputDeviceVolumes()
    {
        return this.GetDeviceVolumes(DataFlow.Capture);
    }

    public ChangeAudioDeviceVolumeModel ChangeMuteOnOutputDevice(string id, bool mute)
    {
        return this.ChangeMuteOnDevice(id, mute, DataFlow.Render);
    }

    public ChangeAudioDeviceVolumeModel ChangeMuteOnInputDevice(string id, bool mute)
    {
        return this.ChangeMuteOnDevice(id, mute, DataFlow.Capture);
    }

    public ChangeAudioDeviceVolumeModel ChangeVolumeOnOutputDevice(string id, float volumeScalar)
    {
        return this.ChangeVolumeOnDevice(id, volumeScalar, DataFlow.Render);
    }

    public ChangeAudioDeviceVolumeModel ChangeVolumeOnInputDevice(string id, float volumeScalar)
    {
        return this.ChangeVolumeOnDevice(id, volumeScalar, DataFlow.Capture);
    }

    private ChangeAudioDeviceVolumeModel ChangeMuteOnDevice(string id, bool mute, DataFlow type)
    {
        using var enumerator = new MMDeviceEnumerator();

        try
        {
            using var device = enumerator.GetDevice(id);

            if(device.State != DeviceState.Active)
            {
                return new ChangeAudioDeviceVolumeModel(id, AudioRequestResult.DeviceNotConnected);                
            }

            device.AudioEndpointVolume.Mute = mute;

            return new ChangeAudioDeviceVolumeModel(id, device.FriendlyName, device.AudioEndpointVolume.Mute, device.AudioEndpointVolume.MasterVolumeLevelScalar);
        }
        catch (Exception ex)
        {
            return new ChangeAudioDeviceVolumeModel(id, AudioRequestResult.Exception, ex);
        }
    }

    private ChangeAudioDeviceVolumeModel ChangeVolumeOnDevice(string id, float volumeScalar, DataFlow type)
    {
        using var enumerator = new MMDeviceEnumerator();

        try
        {
            using var device = enumerator.GetDevice(id);

            if (device.State != DeviceState.Active)
            {
                return new ChangeAudioDeviceVolumeModel(id, AudioRequestResult.DeviceNotConnected);
            }

            device.AudioEndpointVolume.MasterVolumeLevelScalar = volumeScalar;

            return new ChangeAudioDeviceVolumeModel(id, device.FriendlyName, device.AudioEndpointVolume.Mute, device.AudioEndpointVolume.MasterVolumeLevelScalar);
        }
        catch (Exception ex)
        {
            return new ChangeAudioDeviceVolumeModel(id, AudioRequestResult.Exception, ex);
        }
    }

    private IDictionary<string, AudioDeviceVolumeModel> GetDeviceVolumes(DataFlow type)
    {
        using var enumerator = new MMDeviceEnumerator();

        var endpoints = enumerator.EnumerateAudioEndPoints(
            type,
            DeviceState.Active);

        return endpoints.ToDictionary(x => x.ID, x => x.GetVolumeModelFromMMDeviceAndDispose());
    }

    private IEnumerable<AudioDeviceModel> GetDevices(DataFlow type, bool includeAllDevices) 
    {
        using var enumerator = new MMDeviceEnumerator();

        var endpoints = enumerator.EnumerateAudioEndPoints(
            type,
            includeAllDevices ? DeviceState.All : DeviceState.Active);

        return endpoints.Select(x => x.GetModelFromMMDeviceAndDispose()).ToArray();
    }
}
