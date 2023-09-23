namespace Web;

public class ServerController
{
    private void CheckDirectories()
    {
        if (Directory.GetDirectories(Directory.GetCurrentDirectory())
                .FirstOrDefault(x => x.Split('/')[^1] == Configurator.StaticFilesPath) == null)
        {
            Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}/{Configurator.StaticFilesPath}");
            Console.WriteLine($"Каталог \"{Configurator.StaticFilesPath}\" был создан");
        }

        foreach (var directory in Directory.GetDirectories(Configurator.StaticFilesPath))
        {
            if (Directory.GetFiles($"{directory}/")
                    .FirstOrDefault(x => x.Split('/')[^1] == "index.html") == null)
            {
                throw new FileNotFoundException("index.html");
            }
        }
    }

    public void ServerRun()
    {
        try
        {
            CheckDirectories();
            var serverController = new ServerLauncher();
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