using System.Reflection;
using System.Runtime.InteropServices;

public static class AppInfo
{
    public static string AppName => Assembly.GetEntryAssembly()?.GetName()?.Name ?? "App Name Not Set";

    public static string Version => Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString() ?? "0.0.0.0";

    public static string InstanceName
    {
        get
        {
            var machineName = Environment.MachineName;

            var osPlatform = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? "windows"
                : RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
                ? "macos"
                : RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                ? "linux"
                : "other";

            var bitness = RuntimeInformation.ProcessArchitecture == Architecture.X64
                ? "x86_64"
                : RuntimeInformation.ProcessArchitecture == Architecture.X86
                ? "x86_32"
                : RuntimeInformation.ProcessArchitecture == Architecture.Arm64
                ? "arm64"
                : RuntimeInformation.ProcessArchitecture == Architecture.Arm
                ? "arm"
                : RuntimeInformation.ProcessArchitecture.ToString();

            return $"{osPlatform}_{bitness}_{machineName}";
        }
    }
}
