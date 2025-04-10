using MongoDB.Bson;

namespace DAL.Entities;

public sealed class Office
{
    public ObjectId Id { get; set; }
    public ObjectId? PhotoFileId { get; set; }
    public string City { get; set; } = String.Empty;
    public string Street { get; set; } = String.Empty;
    public string HouseNumber { get; set; } = String.Empty;
    public string? OfficeNumber { get; set; }
    public string RegistryPhoneNumber { get; set; } = String.Empty;
    public bool IsActive { get; set; }
}