namespace Web.Attributes;

public class HttpMethodAttribute: Attribute
{
    public string ActionName { get;}
    
    public HttpMethodAttribute(string actionName)
    {
        ActionName = actionName;
    }
}