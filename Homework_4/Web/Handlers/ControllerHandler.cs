using System.Net;
using System.Reflection;
using Web.Attributes;

namespace Web.Handlers;

public class ControllersHandler: Handler
{
    public override void HandleRequest(HttpListenerContext context)
    {
        var uriSegments = context.Request.Url.Segments;
        string[] strParams = uriSegments
            .Skip(1)
            .Select(s => s.Replace("/", ""))
            .ToArray();
        
        var controllerName = strParams[^2];
        var methodName = strParams[^1];
        
        
        
        // string.Equals(c.Name, controllerName, StringComparison.CurrentCultureIgnoreCase)

        var assembly = Assembly.GetExecutingAssembly();
        var controller = assembly
            .GetTypes()
            .Where(t => Attribute.IsDefined(t, typeof(HttpControllerAttribute)))
            .FirstOrDefault(c =>
                ((HttpControllerAttribute)Attribute.GetCustomAttribute(c, typeof(HttpControllerAttribute))!).Name.Equals(controllerName));


        // WWif (controller == null) return false;

        var method = controller
            .GetMethods()
            .FirstOrDefault(t => t.Name.Equals(methodName, StringComparison.OrdinalIgnoreCase));

        // if (method == null) return false;

        object[] queryParams = method
            .GetParameters()
            .Select((p, i) => Convert.ChangeType(strParams[i], p.ParameterType))
            .ToArray();

        method.Invoke(Activator.CreateInstance(controller), queryParams);
    }
}