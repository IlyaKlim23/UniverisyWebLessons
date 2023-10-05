using System.Net;
using System.Text.Json;
using Web.Models;
using Web.services;

namespace Web.Handlers;

public class PostMethodHandler: Handler
{
    private FormDataModel GetFromPostMethod(HttpListenerContext context)
    {
        string postData;
        var request = context.Request;
        
        using (Stream body = request.InputStream)
        {
            System.Text.Encoding encoding = request.ContentEncoding;
            StreamReader reader = new StreamReader(body, encoding);
            postData = reader.ReadToEnd();
        }

        FormDataModel formDataModel = JsonSerializer.Deserialize<FormDataModel>(postData) ?? throw new Exception();
        
        return formDataModel;
    }
    
    private void SendToEmail(string emailTo, string message)
    {
        var sender = new EmailSenderService();
        sender.SendEmail(emailTo, "Information", message);
    }
    
    public override void HandleRequest(HttpListenerContext context)
    {
        if (context.Request.HttpMethod == "POST")
        {
            var postBody = GetFromPostMethod(context);
            SendToEmail(postBody.GetEmail() ,postBody.ConvertToString());
        }
        else
        {
            Successor.HandleRequest(context);
        }
    }
}