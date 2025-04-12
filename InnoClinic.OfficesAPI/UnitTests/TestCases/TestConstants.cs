using MongoDB.Bson;

namespace UnitTests.TestCases;

public static class TestConstants
{
    public static readonly ObjectId TestOfficeId = ObjectId.GenerateNewId();
    public static readonly ObjectId TestPhotoFileId = ObjectId.GenerateNewId();
    public static readonly byte[] TestPhotoData = new byte[] { 0x01, 0x02, 0x03 };
    public static readonly string TestPhotoFileName = "office.jpg";

    public const string TestCity = "Test City";
    public const string TestStreet = "Test Street";
    public const string TestHouseNumber = "123";
    public const string TestOfficeNumber = "45";
    public const string TestRegistryPhoneNumber = "+1234567890";
}