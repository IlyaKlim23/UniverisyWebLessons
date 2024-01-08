using Npgsql;
using Web.ORM.Entities;

namespace Web.ORM.Repository;

/*
 Это пример репозитория, через него и будет происходить взаимодействие между сущностью и базой данных
 Для каждой модели нужен свой репозиторий
 Просто по аналогии строишь репозиторий. Если нужны будут методы: https://metanit.com/sharp/adonet/ или ChatGpt 
*/
public class ToysRepository
{
    private DbContext.DbContext _context = new();
    
    /// <summary>
    /// Метод получения всех сущностей
    /// </summary>
    /// <returns>Список сущностей</returns>
    public async Task<IEnumerable<Toy>> Select()
    {
        await _context.OpenConnection();
        
        string sqlExpression = "SELECT * FROM toys";

        var command = _context.GetCommand(sqlExpression);

        NpgsqlDataReader reader = command.ExecuteReader();

        List<Toy> result = new List<Toy>();
        
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                Toy toy = new Toy
                {
                    Id = (int)reader["ToyID"],
                    Name = reader["ToyName"].ToString()!,
                    Manufacturer = reader["Manufacturer"].ToString()!,
                    Price = (decimal)reader["Price"],
                    ReleaseDate = (DateTime)reader["ReleaseDate"],
                    LastModified = (DateTime)reader["LastModified"],
                    InStock = (bool)reader["InStock"]
                };
                
                result.Add(toy);
            }
        }

        reader.Close();

        await _context.CloseConnection();
        return result;
    }

    /// <summary>
    /// Метод добавления сущности в базу данных
    /// </summary>
    /// <param name="entity"></param>
    public async Task Add(Toy entity)
    {
        await _context.OpenConnection();

        string sqlExpression = "INSERT INTO Toys (ToyName, Manufacturer, Price, ReleaseDate, LastModified, InStock) VALUES (@ToyName, @Manufacturer, @Price, @ReleaseDate, @LastModified, @InStock)";

        var command = _context.GetCommand(sqlExpression);
        
        command.Parameters.AddWithValue("@ToyName", entity.Name);
        command.Parameters.AddWithValue("@Manufacturer", entity.Manufacturer);
        command.Parameters.AddWithValue("@Price", entity.Price);
        command.Parameters.AddWithValue("@ReleaseDate", entity.ReleaseDate);
        command.Parameters.AddWithValue("@LastModified", entity.LastModified);
        command.Parameters.AddWithValue("@InStock", entity.InStock);

        int rowsAffected = command.ExecuteNonQuery();
        await _context.CloseConnection();
    }
}