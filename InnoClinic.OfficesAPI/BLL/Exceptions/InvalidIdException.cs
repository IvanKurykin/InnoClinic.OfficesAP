namespace BLL.Exceptions;

public class InvalidIdException : Exception
{
    private const string DefaultMessage = "Invalid id.";
    public InvalidIdException() : base(DefaultMessage) { }
    public InvalidIdException(string message) : base(message) { }
    public InvalidIdException(string message, Exception innerException) : base(message, innerException) { }
}