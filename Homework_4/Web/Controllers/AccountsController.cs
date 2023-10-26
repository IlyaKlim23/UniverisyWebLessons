using System.Text;
using Web.Attributes;
using Web.Models;
using Web.services;

namespace Web.Controllers;

[HttpController(name: "account")]
public class AccountsController
{
    public AccountsController() { }

    private static List<FormDataModel> _accounts = new ();

    [Post("SendToEmail")]
    public void SendToEmail(FormDataModel model)
    {
        var sender = new EmailSenderService();
        sender.SendEmail(model.GetEmail(), "Information", model.ConvertToString());
    }

    [Get("GetEmailList")]
    public string GetEmailList()
    {
        var accountList = new StringBuilder();
        foreach (var account in GetAccounts())
        {
            accountList.Append($"<li>{account.GetEmail()}</li>");
        }
        return $"<html><head></head><body><ul>{accountList}</ul></body></html>";
    }
    
    [Get("GetAccountsList")]
    public FormDataModel[] GetAccounts()
    {
        var accounts = new FormDataModel[]
        {
            new ()
            {
                FirstName = "Валера", SecondName = "Тракторист", Email = "valera_krutoy@mail.ru",
                PhoneNumber = "8388383944", BirthDate = "24.01.2026"
            },
            new ()
            {
                FirstName = "Вася", SecondName = "Пупкин", Email = "vasya@mail.ru",
                PhoneNumber = "3498398493", BirthDate = "01.01.1999"
            },
            new ()
            {
                FirstName = "Никита", SecondName = "Самосвал", Email = "nikitch@mail.ru",
                PhoneNumber = "3498398348", BirthDate = "09.10.2002"
            }
        };

        return accounts;
    }
}