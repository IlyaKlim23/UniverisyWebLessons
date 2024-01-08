namespace Web.ORM.Entities;

// Это ПРИМЕР модели которая будет записываться в базу данных, для своих нужд создаешь новые
public class Toy: IEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string Manufacturer { get; set; } = default!;

    public decimal Price { get; set; }

    public DateTime ReleaseDate { get; set; } = default!;
    
    public DateTime LastModified { get; set; } = default!;

    public bool InStock { get; set; }
}