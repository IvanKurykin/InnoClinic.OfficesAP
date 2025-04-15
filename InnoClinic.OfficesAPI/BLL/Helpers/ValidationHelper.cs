using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace BLL.Helpers;

[ExcludeFromCodeCoverage]
public static class ValidationHelper
{
    public static bool BeAValidImage(IFormFile? file)
    {
        if (file is null) return false;

        var allowedExtensions = new[] { ".jpg", ".png"};
        var extension = Path.GetExtension(file.FileName).ToLower();
        return allowedExtensions.Contains(extension);
    }

    public static bool BeAValidNumber(string? number, bool orEmpty = false)
    {
        return (orEmpty && string.IsNullOrEmpty(number))|| (!string.IsNullOrEmpty(number) && number.All(c => char.IsDigit(c) || char.IsLetter(c)));
    }

    public static bool BeAValidName(string name)
    {
        return !string.IsNullOrEmpty(name) && name.All(c => char.IsLetter(c) || c == ' ' || c == '-');
    }
}
