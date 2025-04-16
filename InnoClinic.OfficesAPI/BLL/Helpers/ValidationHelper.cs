using Microsoft.AspNetCore.Http;

namespace BLL.Helpers;

public static class ValidationHelper
{
    public static bool BeAValidImage(IFormFile? file)
    {
        if (file is null) return false;

        var allowedExtensions = new[] { ".jpg", ".png"};
        var extension = Path.GetExtension(file.FileName).ToLower();
        return allowedExtensions.Contains(extension);
    }

    public static bool IsAValidNumber(string? input, bool allowEmpty = false)
    {
        if (string.IsNullOrEmpty(input)) return allowEmpty;

        return input.All(char.IsLetterOrDigit);
    }

    public static bool BeAValidName(string name)
    {
        return !string.IsNullOrEmpty(name) && name.All(c => char.IsLetter(c) || c == ' ' || c == '-');
    }
}
