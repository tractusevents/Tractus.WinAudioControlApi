using Microsoft.AspNetCore.Http.Json;
using Serilog;
using System.Text.Json.Serialization;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

Log.Information($"{AppInfo.AppName}, {AppInfo.Version} - {AppInfo.InstanceName} - Starting up...");

var builder = WebApplication.CreateBuilder(args);

if (args.Any(x => x.StartsWith("p=")))
{
    var portRaw = args.FirstOrDefault(x => x.StartsWith("p=")).Split("=")[1];
    if(int.TryParse(portRaw, out var port) && port > 0 && port < 65535)
    {
        builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
    }
}


builder.Services.AddCors(o =>
{
    o.AddDefaultPolicy(p =>
    {
        p.AllowAnyHeader();
        p.AllowAnyMethod();
        p.AllowAnyOrigin();
    });
});

builder.Services.AddSerilog();

builder.Services.Configure<JsonOptions>(o =>
{
    o.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer().AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = $"{AppInfo.AppName}"
    });
});


var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{

});

app.UseCors();

app.MapGet("/outputs", (HttpContext context) =>
{
    var showAll = !string.IsNullOrEmpty(context.Request.Query["all"]);
    return WindowsAudioInfoController.Instance.GetOutputDevices(showAll);
});

app.MapGet("/outputs/status", (HttpContext context) =>
{
    return WindowsAudioInfoController.Instance.GetOutputDeviceVolumes();
});

app.MapGet("/inputs", (HttpContext context) =>
{
    var showAll = !string.IsNullOrEmpty(context.Request.Query["all"]);
    return WindowsAudioInfoController.Instance.GetInputDevices(showAll);
});

app.MapGet("/inputs/status", (HttpContext context) =>
{
    return WindowsAudioInfoController.Instance.GetInputDeviceVolumes();
});

app.MapGet("/output/{id}/mute", (string id) =>
{
    return WindowsAudioInfoController.Instance.ChangeMuteOnOutputDevice(
        id, true);
});

app.MapGet("/output/{id}/unmute", (string id) =>
{
    return WindowsAudioInfoController.Instance.ChangeMuteOnOutputDevice(
        id, false);
});

app.MapGet("/input/{id}/mute", (string id) =>
{
    return WindowsAudioInfoController.Instance.ChangeMuteOnInputDevice(
        id, true);
});

app.MapGet("/input/{id}/unmute", (string id) =>
{
    return WindowsAudioInfoController.Instance.ChangeMuteOnInputDevice(
        id, false);
});

app.MapGet("/input/{id}/volume/{level}", (string id, float level) =>
{
    return WindowsAudioInfoController.Instance.ChangeVolumeOnInputDevice(
        id, level);
});

app.MapGet("/output/{id}/volume/{level}", (string id, float level) =>
{
    return WindowsAudioInfoController.Instance.ChangeVolumeOnOutputDevice(
        id, level);
});

app.MapGet("/appinfo", () =>
{
    return AppInfoModel.Details;
});

app.Run();