using MongoDB.Bson;

namespace DAL.Entities;

public sealed class Office
{
    public ObjectId Id { get; set; }
    public ObjectId? PhotoFileId { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? HouseNumber { get; set; }
    public string? OfficeNumber { get; set; }
    public string? RegistryPhoneNumber { get; set; } 
    public bool IsActive { get; set; }
}