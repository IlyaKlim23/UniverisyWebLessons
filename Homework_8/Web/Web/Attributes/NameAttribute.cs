namespace Web.Attributes;

public class NameAttribute : Attribute
{
    public string Value { get; }

    public NameAttribute(string value)
    {
        Value = value;
    }
}