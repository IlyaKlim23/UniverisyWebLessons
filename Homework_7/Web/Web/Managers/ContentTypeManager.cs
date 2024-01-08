namespace Web;

public static class ContentTypeManager
{
    private static Dictionary<string, string> _supportedType = new Dictionary<string, string>()
    {
        {".svg", "image/svg+xml"},
        {".png", "image/png"},
        {".html","text/html"}
    };

    public static string? GetContentType(string path) =>
        _supportedType
            .TryGetValue(Path.GetExtension(path).ToLower(), out string? type) 
            ? type 
            : null;
}