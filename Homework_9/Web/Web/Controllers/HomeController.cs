using Web.Attributes;
using Web.ORM.Entities;
using Web.ORM.Repository;

namespace Web.Controllers;

/// Всегда пишешь этот атрибут и имя, по которому будет поиск
[HttpController(name: "Home")]
public class HomeController
{
    // Таким образом обращаешься к бд
    private static ToysRepository _repo = new ToysRepository();

    // Когда пишешь медод, привязываешь к нему атрибут [Get/Post("Название, по которому будет искаться метод")]
}