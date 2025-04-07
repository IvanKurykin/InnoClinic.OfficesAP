using MongoDB.Bson;

namespace DAL.Entities;

public sealed class Office
{
    public ObjectId Id { get; set; }    
    public byte[]? Photo { get; set; }  
    public string City { get; set; }
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public string OfficeNumber { get; set; }
    public string RegistryPhoneNumber { get; set; } 
    public bool Status { get; set; }
    public string Address { get; set; }

    public Office(string city, string street, string houseNumber, string officeNumber, string registryPhoneNumber, bool status)
    {
        City = city;
        Street = street;
        HouseNumber = houseNumber;
        OfficeNumber = officeNumber;
        RegistryPhoneNumber = registryPhoneNumber;
        Status = status;
        Address = $"{City}, {Street}, {HouseNumber}, {OfficeNumber}";
    }
}
