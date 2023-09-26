using System.Runtime;
using System.Text.Json.Serialization;

namespace Web.Configuration;

public class AppSettings
{
    public string Address { get; set; }
    public int Port { get; set; }
    public string StaticFilesPath { get; set; }
}