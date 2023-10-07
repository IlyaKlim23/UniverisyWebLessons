using Web.Attributes;
using Web.Models;
using Web.services;

namespace Web.Controllers;

[HttpController(name: "account")]
public class AccountsController
{
    public AccountsController() { }

    public void Add(string info)
    {
        Console.WriteLine(info);
        // var sender = new EmailSenderService();
        // var emailTo = model.GetEmail();
        // var message = model.ConvertToString();
        // sender.SendEmail(emailTo, "Information", message);
    }
    
    public void Delete() {}
    
    public void Select() {}
    
    public void Update() {}
    
    public void SelectByEmail() {}
}