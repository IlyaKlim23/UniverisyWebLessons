
using Web.ORM.DbContext;
using Web.ORM.Entities;
using Web.ORM.Repository;

/// Тестовый файлик, использую для проверки работы методов ORM
var dbc = new DbContext();

var rep = new ToysRepository();

var r = await rep.Select();

foreach (var s in r)
{
    Console.WriteLine(s.Name);
}