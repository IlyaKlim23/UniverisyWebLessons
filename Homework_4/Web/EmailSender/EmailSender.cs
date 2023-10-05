using System.IO.Compression;
using System.Net;
using System.Net.Mail;
using Web.Configuration;
using Web.ZipCompressor;

namespace Web;

public static class EmailSender
{
    public static async void SendMessageAsync(EmailFromData emailFromData, string emailTo, string? messageSubject, string messageBody)
    {
        try
        {
            Console.WriteLine("Сбор сообщения...");
            
            ZipCompressor.ZipCompressor.Compress("../../../../");
            
            var from = new MailAddress(emailFromData.EmailFrom ?? throw new ArgumentNullException(emailFromData.EmailFrom),
                emailFromData.DisplayName);
            var to = new MailAddress(emailTo);
            
            var message = new MailMessage(from, to);
            message.Subject = messageSubject ?? "";
            message.Body = messageBody;
            message.Attachments.Add(new Attachment(ZipCompressor.ZipCompressor.AbsoluteZipFilePath));
            
            var smtp = new SmtpClient(emailFromData.Host ?? throw new ArgumentNullException(emailFromData.Host),
                emailFromData.Port ?? throw new ArgumentNullException($"{emailFromData.Port}"));
            
            smtp.Credentials = new NetworkCredential(emailFromData.EmailFrom.Split('@')[0],
                emailFromData.Password ?? throw new ArgumentNullException(emailFromData.Password));
            smtp.EnableSsl = true;
            Console.WriteLine("Отправка сообщения...");
            await smtp.SendMailAsync(message);
            if (Task.CompletedTask.IsCompleted)
            {
                Console.WriteLine("Сообщение отправлено");
                ZipCompressor.ZipCompressor.RemoveZip();
            }
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
}