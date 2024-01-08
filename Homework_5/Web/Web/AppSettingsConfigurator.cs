using System.Text.Json;
using Web.Configuration;

namespace Web;

public class AppSettingsConfigurator
{
    private static readonly AppSettingsConfigurator instance = new AppSettingsConfigurator();

    private AppSettings Configuration { get; set; }

    public static string Address { get; private set; }
    public static int Port { get; private set; }
    public static string StaticFilesPath { get; private set; }
    
    private AppSettingsConfigurator()
    {
        Configuration = GetConfig();
        Address = Configuration.Address;
        Port = Configuration.Port;
        StaticFilesPath = $@"..\..\..\{Configuration.StaticFilesPath}"; 
    }

    private static AppSettings GetConfig()
    {
        using var file = File.OpenRead(@"..\..\..\appsettings.json"); 
        return JsonSerializer.Deserialize<AppSettings>(file) ?? throw new Exception();
    }
}