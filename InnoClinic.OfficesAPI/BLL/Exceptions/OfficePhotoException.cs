namespace BLL.Exceptions;

public class OfficePhotoException : Exception
{
    private const string DefaultMessage = "Office photo processing error.";
    public OfficePhotoException() : base(DefaultMessage) { }
    public OfficePhotoException(string message) : base(message) { }
    public OfficePhotoException(string message, Exception innerException) : base(message, innerException) { }
}
