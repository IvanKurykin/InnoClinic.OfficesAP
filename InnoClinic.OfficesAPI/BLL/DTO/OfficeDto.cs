using MongoDB.Bson;

namespace BLL.DTO;

public sealed record class OfficeDto
{
    public string Id { get; set; } = string.Empty;
    public byte[]? Photo { get; set; }
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string HouseNumber { get; set; } = string.Empty;
    public string? OfficeNumber { get; set; }
    public string RegistryPhoneNumber { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string Address => $"{City}, {Street}, {HouseNumber}, {OfficeNumber}";
}