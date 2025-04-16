using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace BLL.Exceptions;

[ExcludeFromCodeCoverage]
public class OfficeNotFoundException : OfficeException
{
    private const string DefaultMessage = "The office was not found.";
    private const int OfficeNotFoundExceptionStatusCode = StatusCodes.Status404NotFound;

    public OfficeNotFoundException() : base(DefaultMessage, OfficeNotFoundExceptionStatusCode) { }
    public OfficeNotFoundException(string message) : base(message, OfficeNotFoundExceptionStatusCode) { }
}