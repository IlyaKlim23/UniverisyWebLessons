namespace Web;

public static class ServerController
{
    private static bool _isRunning = true;
    
    private static void CheckDirectories()
    {
        if (Directory.GetDirectories(Directory.GetCurrentDirectory())
                .FirstOrDefault(x => x.Split('/')[^1] == AppSettingsConfigurator.StaticFilesPath) == null)
        {
            Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}/{AppSettingsConfigurator.StaticFilesPath}");
            Console.WriteLine($"Каталог \"{AppSettingsConfigurator.StaticFilesPath}\" был создан");
        }

        foreach (var directory in Directory.GetDirectories(AppSettingsConfigurator.StaticFilesPath))
        {
            if (Directory.GetFiles($"{directory}/")
                    .FirstOrDefault(x => x.Split('/')[^1] == "index.html") == null)
            {
                throw new FileNotFoundException("index.html");
            }
        }
    }

    public static void ServerRun()
    {
        Console.CancelKeyPress += delegate (object? sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            _isRunning = false;
        };
        
        try
        {
            CheckDirectories();
            using (var server = new HttpServer())
            {
                server.Start();

                while (Console.ReadLine() != "stop" && _isRunning)
                {
                    
                }
            }
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