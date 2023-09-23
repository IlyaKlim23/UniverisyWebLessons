using System.Net;

namespace Web;

public class ServerLauncher
{
    private readonly HttpListener _server;
    
    public ServerLauncher()
    {
        _server = new HttpListener();
        SetListenerAddress();
    }

    private void SetListenerAddress()
    {
        _server.Prefixes.Add($"http://{Configurator.Address}:{Configurator.Port}/");
    }

    private string GetUrl(HttpListenerContext context)
    {
        var url = context.Request.Url?.AbsolutePath;

        if (url == null) throw new ArgumentNullException(url);

        if (url.Split('.')[^1] == "html" && Directory.GetFiles($"{Configurator.StaticFilesPath}/")
                .FirstOrDefault(x => x.Split('/')[^1] == url.Trim('/')) == null)
        {
            url = "/not_found_page.html";
        }

        return url;
    }

    private void KeepServerRunningAsync()
    {
        while (_server.IsListening)
        {
            var context = _server.GetContext();

            var url = GetUrl(context);

            var response = context.Response;

            SendData(response, url);
        }
    }

    private async void SendData(HttpListenerResponse response, string url)
    {
        var str = await File.ReadAllBytesAsync($"{Configurator.StaticFilesPath}/{url}");

        response.ContentLength64 = str.Length;
        await using var output = response.OutputStream;

        await output.WriteAsync(str);
        await output.FlushAsync();
    }

    public void StartServer()
    {
        Console.WriteLine("Запуск сервера");
        _server.Start();
        Console.WriteLine("Сервер успешно запущен");
        KeepServerRunningAsync();
        _server.Stop();
    }
}