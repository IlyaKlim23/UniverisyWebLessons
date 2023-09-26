using System.Net;
using System.Net.Mail;
using System.Text.Json;
using Web.Configuration;

namespace Web;

public class EmailSender
{
    private string? EmailFrom { get; set; }
    private string? DisplayName { get; set; }
    private string? Password { get; set; }
    private string? EmailTo { get; set; }

    private const string Host = "smtp.yandex.ru";

    private const int Port = 25;

    public EmailSender()
    {
        var emailData = GetEmailData();
        EmailFrom = emailData.EmailFrom;
        EmailTo = emailData.EmailTo;
        DisplayName = emailData.DisplayName;
        Password = emailData.Password;
    }

    public void SendMessage(string? messageSubject, string messageBody)
    {
        try
        {
            Console.WriteLine("Сбор сообщения...");
            var from = new MailAddress(EmailFrom ?? throw new ArgumentNullException(EmailFrom),
                DisplayName);
            var to = new MailAddress(EmailTo ?? throw new ArgumentNullException(EmailTo));

            var message = new MailMessage(from, to);
            message.Subject = messageSubject ?? "";
            message.Body = messageBody;

            var smtp = new SmtpClient(Host, Port);
            smtp.Credentials = new NetworkCredential(EmailFrom.Split('@')[0],
                Password ?? throw new ArgumentNullException(Password));
            smtp.EnableSsl = true;
            Console.WriteLine("Отправка сообщения...");
            smtp.Send(message);
            Console.WriteLine("Сообщение отправлено");
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine($"Ошибка: параметр {e.ParamName} пуст");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            Console.WriteLine("Процесс завершен");
        }
    }

    private EmailData GetEmailData()
    {
        using var file = File.OpenRead("EmailData.json"); 
        return JsonSerializer.Deserialize<EmailData>(file) ?? throw new Exception();
    }
}