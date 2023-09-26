using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using Web.Attributes;

namespace Web.Models;

public interface IDataModel
{
    string ConvertToString();
}

public class FormDataModel: IDataModel
{
    [Name("Имя")]
    public string FirstName { get; set; }

    [Name("Фамилия")]
    public string SecondName { get; set; }

    [Name("Дата Рождения")]
    public string BirthDate { get; set; }

    [Name("Номер Телефона")]
    public string PhoneNumber { get; set; }
    
    [Name("Ссылка на соц. сеть")]
    public string SocialNetworkLink { get; set; }
    
    public string ConvertToString()
    {
        var result = new StringBuilder();
        foreach (var field in typeof(FormDataModel).GetProperties())
        {
            result.Append($"{((NameAttribute)field.GetCustomAttribute(typeof(NameAttribute), false)!).Value}: {field.GetValue(this)}");
            result.Append('\n');
        }
        return result.ToString();
    }
}