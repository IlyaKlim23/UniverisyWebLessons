using System.Text.Json;
using Web.Configuration;

namespace Web;

public class Configurator
{
    private readonly AppSettings _config;

    public Configurator(string path)
    {
        using var file = File.OpenRead(path);
        _config = JsonSerializer.Deserialize<AppSettings>(file) ?? throw new Exception();
    }

    public string GetAddress() => _config.Address;

    public int GetPort() => _config.Port;

    public string GetStaticFilesPath() => _config.StaticFilesPath;
}