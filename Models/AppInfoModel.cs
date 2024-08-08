public class AppInfoModel
{
    public string AppName { get; set; }
    public string Version { get; set; }
    public string InstanceType { get; set; }

    public static AppInfoModel Details { get; }
        = new AppInfoModel();

    private AppInfoModel()
    {
        this.AppName = AppInfo.AppName;
        this.Version = AppInfo.Version;
        this.InstanceType = AppInfo.InstanceName;
    }
}