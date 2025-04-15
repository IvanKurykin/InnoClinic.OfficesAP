using Microsoft.AspNetCore.Http;

namespace BLL.Exceptions;

public class OfficePhotoException : OfficeException
{
    private const string DefaultMessage = "Office photo processing error.";
    private const int OfficePhotoExceptionStatusCode = StatusCodes.Status400BadRequest;

    public OfficePhotoException() : base(DefaultMessage, OfficePhotoExceptionStatusCode) { }
    public OfficePhotoException(string message) : base(message, OfficePhotoExceptionStatusCode) { }
}