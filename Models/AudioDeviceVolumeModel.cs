public class AudioDeviceVolumeModel
{
    public string ID { get; set; }
    public string FriendlyName { get; set; }
    public bool Muted { get; set; }
    public float VolumeScalar { get; set; }

    public AudioDeviceVolumeModel(string id, string friendlyName, bool muted, float volumeScalar)
    {
        this.ID = id;
        this.FriendlyName = friendlyName;
        this.Muted = muted;
        this.VolumeScalar = volumeScalar;
    }
}
