using Microsoft.AspNetCore.Http;

namespace BLL.DTO;

public sealed class OfficeRequestDto
{
    public IFormFile? Photo { get; set; }
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string HouseNumber { get; set; } = string.Empty;
    public string? OfficeNumber { get; set; }
    public string RegistryPhoneNumber { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}