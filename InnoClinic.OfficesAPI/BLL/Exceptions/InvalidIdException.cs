﻿using Microsoft.AspNetCore.Http;

namespace BLL.Exceptions;

public class InvalidIdException : OfficeException
{
    private const string DefaultMessage = "Invalid id.";
    private const int InvalidIdExceptionStatusCode = StatusCodes.Status400BadRequest;

    public InvalidIdException() : base(DefaultMessage, InvalidIdExceptionStatusCode) { }
    public InvalidIdException(string message) : base(message, InvalidIdExceptionStatusCode) { }
}