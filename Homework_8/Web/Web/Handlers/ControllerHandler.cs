using System.Collections.Specialized;
using System.Net;
using System.Reflection;
using System.Text;
using Web.Attributes;
using Web.ORM.Entities;

namespace Web.Handlers;

public class ControllersHandler: Handler
{
    public override void HandleRequest(HttpListenerContext context)
    {
        var uriSegments = context.Request.Url!.Segments;
        
        string[] strParams = uriSegments
            .Skip(1)
            .Select(s => s.Replace("/", ""))
            .ToArray();
        
        if (strParams.Length < 2)
            return;
        
        var controllerName = strParams[^2];
        var methodName = strParams[^1];
        
        
        
        // string.Equals(c.Name, controllerName, StringComparison.CurrentCultureIgnoreCase)

        var assembly = Assembly.GetExecutingAssembly();
        var tt = assembly.GetTypes();
        var controller = assembly
            .GetTypes()
            .Where(t => Attribute.IsDefined(t, typeof(HttpControllerAttribute)))
            .FirstOrDefault(c =>
                ((HttpControllerAttribute)Attribute.GetCustomAttribute(c, typeof(HttpControllerAttribute))!).Name.Equals(controllerName));


        // WWif (controller == null) return false;

        var method = controller?
            .GetMethods()
            .FirstOrDefault(x => x.GetCustomAttributes(true)
                .Any(attr => attr.GetType().Name.Equals($"{context.Request.HttpMethod}Attribute",
                                 StringComparison.OrdinalIgnoreCase) 
                             && ((HttpMethodAttribute)attr).ActionName.Equals(methodName, StringComparison.OrdinalIgnoreCase)));

        // if (method == null) return false;

        object[] queryParams = Array.Empty<object>();
        
        if(context.Request.HttpMethod == "POST")
            queryParams = GetFromPostMethod(context);
        
        var result = method?.Invoke(Activator.CreateInstance(controller!), queryParams);
        context.Response.ContentType = "text/html";
        if(context.Request.HttpMethod != "POST")
        {
            byte[] buffer = Encoding.UTF8.GetBytes((string)result!);
            context.Response.ContentLength64 = buffer.Length;
            using Stream output = context.Response.OutputStream;
            output.Write(buffer);
            output.Flush();
        }
    }
    
    private object[] GetFromPostMethod(HttpListenerContext context)
    {
        string postData;
        var request = context.Request;
        
        using (Stream body = request.InputStream)
        {
            Encoding encoding = request.ContentEncoding;
            StreamReader reader = new StreamReader(body, encoding);
            postData = reader.ReadToEnd();
        }
        
        NameValueCollection queryParams = System.Web.HttpUtility.ParseQueryString(postData);

        Toy toy = new Toy
        {
            Name = queryParams["Name"]!,
            Manufacturer = queryParams["Manufacturer"]!,
            Price = Convert.ToDecimal(queryParams["Price"]),
            ReleaseDate = Convert.ToDateTime(queryParams["ReleaseDate"]),
            LastModified = Convert.ToDateTime(queryParams["LastModified"]),
            InStock = queryParams["InStock"] == "on"
        };
        
        return new object[]{toy};
    }
}