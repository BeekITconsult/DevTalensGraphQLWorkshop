namespace SweetLemons.Exceptions;

public class ValidationException : Exception
{
    public string PropertyName { get; }

    public ValidationException(string msg, string propertyName)
        : base(msg)
    {
        PropertyName = propertyName;
    }
}