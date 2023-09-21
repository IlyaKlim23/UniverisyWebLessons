namespace Web;

public class ServerController
{
    private readonly Configurator _configurator;

    public ServerController(string settingsPath)
    {
        _configurator = new Configurator(settingsPath);
    }

    private void CheckDirectories()
    {
        if (Directory.GetDirectories(Directory.GetCurrentDirectory())
                .FirstOrDefault(x => x.Split('/')[^1] == _configurator.GetStaticFilesPath()) == null)
        {
            Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}/{_configurator.GetStaticFilesPath()}");
            Console.WriteLine($"Каталог \"{_configurator.GetStaticFilesPath()}\" был создан");
        }

        if (Directory.GetFiles($"{_configurator.GetStaticFilesPath()}/")
                .FirstOrDefault(x => x.Split('/')[^1] == "index.html") == null)
        {
            throw new FileNotFoundException("index.html");
        }
    }

    public void ServerRun()
    {
        try
        {
            CheckDirectories();
            var serverController = new ServerLauncher(_configurator);
            serverController.StartServer();
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Файл {ex.Message} не найден");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Сервер завершил свою работу");
        }
    }
}