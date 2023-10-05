using System.Net;
using Web.Handlers;

namespace Web;

public class HttpServer : IDisposable
{
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly HttpListener _server;
    private readonly Handler _postMethodHandler;
    private readonly Handler _staticHandler;
    private readonly Handler _controllersHandler;

    public HttpServer()
    {
        _server = new HttpListener();
        _server.Prefixes
            .Add($"http://{AppSettingsConfigurator.Address}:{AppSettingsConfigurator.Port}/");
        _cancellationTokenSource = new CancellationTokenSource();

        _postMethodHandler = new PostMethodHandler();
        _staticHandler = new StaticFilesHandler();
        _controllersHandler = new ControllersHandler();
    }

    private void Listen()
    {
        while (_server.IsListening)
        {
            var context = _server.GetContext();
            
            _postMethodHandler.Successor = _staticHandler;
            _staticHandler.Successor = _controllersHandler;
            _postMethodHandler.HandleRequest(context);
        }
    }

    public async Task Start()
    {
        Console.WriteLine("Запуск сервера");
        _server.Start();
        Console.WriteLine("Сервер успешно запущен");

        await Task.Run(Listen, _cancellationTokenSource.Token);
    }

    public void Dispose()
    {
        _cancellationTokenSource.Cancel();
        _server.Stop();
    }
}