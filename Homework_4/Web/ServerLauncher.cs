using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection;
using System.Text.Json;
using Web.Attributes;
using Web.Models;

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
        var url = context.Request.Url?.AbsolutePath.TrimEnd('/');

        if (url == null) throw new ArgumentNullException(url);
        
        if (url.Split('.')[^1] == "html" && url.Split('/').Length == 3)
        {
            if (Directory.GetFiles($"{Configurator.StaticFilesPath}/{url.Split('/')[^2]}")
                    .FirstOrDefault(x => x.Split('/')[^1] == url.Split('/')[^1]) == null)
                url = $"/{url.Split('/')[^2]}" + "/not_found_page.html";
        }

        else if (Directory.GetDirectories(Configurator.StaticFilesPath)
                     .FirstOrDefault(x => x.Split('/')[^1] == url.Split('/')[^1]) != null)
            {
                url += "/index.html";
            }
        

        url = string.Join('/', url.Split('/').ToHashSet());
        return url;
    }

    private void KeepServerRunningAsync()
    {
        while (_server.IsListening)
        {
            var context = _server.GetContext();

            var url = GetUrl(context);

            var response = context.Response;
            var request = context.Request;
            if (request.HttpMethod == "POST")
            {
                var postBody = GetFromPostMethod(request);
                SendToEmail(postBody);
            }

            SendData(response, url);
        }
    }

    private string GetFromPostMethod(HttpListenerRequest request)
    {
        string postData;
        using (Stream body = request.InputStream)
        {
            System.Text.Encoding encoding = request.ContentEncoding;
            StreamReader reader = new StreamReader(body, encoding);
            postData = reader.ReadToEnd();
        }

        FormDataModel formDataModel = JsonSerializer.Deserialize<FormDataModel>(postData) ?? throw new Exception();
        
        return formDataModel.ConvertToString();
    }

    private void SendToEmail(string message)
    {
        var sender = new EmailSender();
        sender.SendMessage("Information", message);
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