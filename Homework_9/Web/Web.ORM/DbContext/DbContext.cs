using Npgsql;

namespace Web.ORM.DbContext;

// Это контекст базы данных, здесь меняешь только строку подключения на свою
public class DbContext
{
    // Строка подключения!!!
    private const string ConnectionString = "Host=localhost;Username=postgres;Password=1234;Database=KidShop";
    private static readonly NpgsqlConnection Connection = new (ConnectionString);

    public async Task OpenConnection()
    {
        await Connection.OpenAsync();
        Console.WriteLine("Подключение открыто");
    }

    public async Task CloseConnection()
    {
        await Connection.CloseAsync();
        Console.WriteLine("Подключение закрыто");
    }

    public NpgsqlCommand GetCommand(string exp)
    {
        NpgsqlCommand command = new NpgsqlCommand(exp, Connection);
        return command;
    }
}