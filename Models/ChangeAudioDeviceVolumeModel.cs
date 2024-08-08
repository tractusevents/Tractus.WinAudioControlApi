using System.Reflection;
using System.Runtime.InteropServices;

public class ChangeAudioDeviceVolumeModel
{
    public string ID { get; set; }
    public string? FriendlyName { get; set; }
    public bool? Muted { get; set; }
    public float? VolumeScalar { get; set; }
    public AudioRequestResult Status { get; set; }
    public string? Error { get; set; }

    public ChangeAudioDeviceVolumeModel(
        string id,
        string friendlyName,
        bool muted,
        float volumeScalar,
        AudioRequestResult status = AudioRequestResult.OK,
        Exception? error = null)
    {
        this.ID = id;
        this.FriendlyName = friendlyName;
        this.Muted = muted;
        this.VolumeScalar = volumeScalar;
        this.Status = status;
        this.Error = error?.Message;
    }

    public ChangeAudioDeviceVolumeModel(
        string id,
        AudioRequestResult status,
        Exception? error = null)
    {
        this.ID = id;
        this.Status = status;
        this.Error = error?.Message;
    }
}
