namespace BLL.Exceptions;

public abstract class OfficeException : Exception
{
    public int HttpStatusCode { get; }

    protected OfficeException(string message, int statusCode)
        : base(message)
    {
        HttpStatusCode = statusCode;
    }
}