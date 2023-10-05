namespace Web.services;

public interface IEmailSenderService
{
    void SendEmail(string emailTo, string? messageSubject, string messageBody);
}