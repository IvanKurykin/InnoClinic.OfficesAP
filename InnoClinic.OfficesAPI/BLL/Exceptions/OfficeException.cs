using System.Diagnostics.CodeAnalysis;

namespace BLL.Exceptions;

[ExcludeFromCodeCoverage]
public abstract class OfficeException : Exception
{
    public int HttpStatusCode { get; }

    protected OfficeException(string message, int statusCode)
        : base(message)
    {
        HttpStatusCode = statusCode;
    }
}