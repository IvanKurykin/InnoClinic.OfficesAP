using Microsoft.AspNetCore.Http;

namespace BLL.Helpers;

public static class ValidationHelper
{
    public static bool BeAValidImage(IFormFile? file)
    {
        if (file is null) return true;

        var allowedExtensions = new[] { ".jpg", ".png"};
        var extension = Path.GetExtension(file.FileName).ToLower();
        return allowedExtensions.Contains(extension);
    }

    public static bool BeAValidNumber(string number)
    {
        return !string.IsNullOrEmpty(number) && number.All(c => char.IsDigit(c) || char.IsLetter(c));
    }

    public static bool BeAValidNumberOrEmpty(string? number)
    {
        return string.IsNullOrEmpty(number) || BeAValidNumber(number);
    }

    public static bool BeAValidName(string name)
    {
        return !string.IsNullOrEmpty(name) && name.All(c => char.IsLetter(c) || c == ' ' || c == '-');
    }
}
