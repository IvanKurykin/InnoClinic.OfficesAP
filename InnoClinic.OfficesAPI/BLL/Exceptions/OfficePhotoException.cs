using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace BLL.Exceptions;

[ExcludeFromCodeCoverage]
public class OfficePhotoException : OfficeException
{
    private const string DefaultMessage = "Office photo processing error.";
    private const int OfficePhotoExceptionStatusCode = StatusCodes.Status400BadRequest;

    public OfficePhotoException() : base(DefaultMessage, OfficePhotoExceptionStatusCode) { }
    public OfficePhotoException(string message) : base(message, OfficePhotoExceptionStatusCode) { }
}