using System.Text.Json;
using Web.Configuration;

namespace Web.services;

public class EmailSenderService: IEmailSenderService
{
    private readonly EmailFromData _emailFromData;
    
    public EmailSenderService()
    {
        _emailFromData = GetEmailData();
    }

    public async void SendEmail(string emailTo, string? messageSubject, string messageBody)
    {
        await Task.Run(()=>
        {
            EmailSender.SendMessageAsync(_emailFromData, emailTo, messageSubject, messageBody);
        });
    }
    
    private EmailFromData GetEmailData()
    {
        using var file = File.OpenRead("EmailData.json"); 
        return JsonSerializer.Deserialize<EmailFromData>(file) ?? throw new Exception();
    }
}