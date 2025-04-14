namespace BLL.Exceptions;

public class OfficeNotFoundException : Exception
{
    private const string DefaultMessage = "The office was not found.";
    public OfficeNotFoundException() : base(DefaultMessage) { }
    public OfficeNotFoundException(string message) : base(message) { }
    public OfficeNotFoundException(string message, Exception innerException) : base(message, innerException) { }
}
