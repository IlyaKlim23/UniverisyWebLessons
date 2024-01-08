namespace Web.Attributes;

public class PostAttribute: HttpMethodAttribute
{
    public PostAttribute(string actionName) : base(actionName)
    {
    }
}