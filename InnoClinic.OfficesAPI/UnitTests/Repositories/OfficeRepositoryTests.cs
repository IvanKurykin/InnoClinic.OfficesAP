using DAL.Entities;
using DAL.Repositories;
using FluentAssertions;
using MongoDB.Driver;
using Moq;
using UnitTests.TestCases;

namespace UnitTests.Repositories;

public class OfficeRepositoryTests
{
    private readonly Mock<IMongoCollection<Office>> _collectionMock;
    private readonly OfficeRepository _officeRepository;
    private readonly Mock<IAsyncCursor<Office>> _cursorMock;

    public OfficeRepositoryTests()
    {
        _collectionMock = new Mock<IMongoCollection<Office>>();
        _cursorMock = new Mock<IAsyncCursor<Office>>();
        _officeRepository = new OfficeRepository(_collectionMock.Object);

        _cursorMock.SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true).ReturnsAsync(false);
    }

    [Fact]
    public async Task CreateOfficeAsyncShouldInsertOffice()
    {
        var office = new Office { Id = TestConstants.TestOfficeId, City = TestConstants.TestCity, Street = TestConstants.TestStreet, HouseNumber = TestConstants.TestHouseNumber, RegistryPhoneNumber = TestConstants.TestRegistryPhoneNumber, IsActive = true };

        _collectionMock.Setup(x => x.InsertOneAsync(office, null, It.IsAny<CancellationToken>())) .Returns(Task.CompletedTask);

        var result = await _officeRepository.CreateOfficeAsync(office);

        result.Should().BeEquivalentTo(office);
        _collectionMock.Verify(x => x.InsertOneAsync(office, null, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetOfficesAsyncShouldReturnOfficesList()
    {
        var expectedOffices = new List<Office>
        {
            new(){ Id = TestConstants.TestOfficeId, City = TestConstants.TestCity, Street = TestConstants.TestStreet, HouseNumber = TestConstants.TestHouseNumber, RegistryPhoneNumber = TestConstants.TestRegistryPhoneNumber, IsActive = true }
        };

        var filter = Builders<Office>.Filter.Empty;

        _cursorMock.Setup(_ => _.Current).Returns(expectedOffices);

        _collectionMock.Setup(x => x.FindAsync(filter, It.IsAny<FindOptions<Office, Office>>(), It.IsAny<CancellationToken>())).ReturnsAsync(_cursorMock.Object);

        var result = await _officeRepository.GetOfficesAsync(filter);

        result.Should().BeEquivalentTo(expectedOffices);
    }

    [Fact]
    public async Task GetOfficeByIdAsyncShouldReturnSingleOffice()
    {
        var expectedOffice = new Office { Id = TestConstants.TestOfficeId, City = TestConstants.TestCity, Street = TestConstants.TestStreet, HouseNumber = TestConstants.TestHouseNumber, RegistryPhoneNumber = TestConstants.TestRegistryPhoneNumber, IsActive = true };

        var filter = Builders<Office>.Filter.Eq(o => o.Id, TestConstants.TestOfficeId);

        _cursorMock.Setup(_ => _.Current).Returns(new List<Office> { expectedOffice });

        _collectionMock.Setup(x => x.FindAsync(filter, It.IsAny<FindOptions<Office, Office>>(), It.IsAny<CancellationToken>())).ReturnsAsync(_cursorMock.Object);

        var result = await _officeRepository.GetOfficeByIdAsync(filter);

        result.Should().BeEquivalentTo(expectedOffice);
    }

    [Fact]
    public async Task UpdateOfficeAsyncShouldReplaceOffice()
    {
        var office = new Office { Id = TestConstants.TestOfficeId, City = TestConstants.TestCity, Street = TestConstants.TestStreet, HouseNumber = TestConstants.TestHouseNumber, RegistryPhoneNumber = TestConstants.TestRegistryPhoneNumber, IsActive = true };

        var filter = Builders<Office>.Filter.Eq(o => o.Id, TestConstants.TestOfficeId);

        var replaceResult = new ReplaceOneResult.Acknowledged(1, 1, null);

        _collectionMock.Setup(x => x.ReplaceOneAsync(filter, office, It.IsAny<ReplaceOptions>(), It.IsAny<CancellationToken>())) .ReturnsAsync(replaceResult);

        var result = await _officeRepository.UpdateOfficeAsync(filter, office);

        result.Should().BeEquivalentTo(office);

        _collectionMock.Verify(x => x.ReplaceOneAsync(filter, office, It.IsAny<ReplaceOptions>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteOfficeAsyncShouldCallDelete()
    {
        var filter = Builders<Office>.Filter.Eq(o => o.Id, TestConstants.TestOfficeId);

        var deleteResult = new DeleteResult.Acknowledged(1);

        _collectionMock.Setup(x => x.DeleteOneAsync(filter, It.IsAny<CancellationToken>())) .ReturnsAsync(deleteResult);

        await _officeRepository.DeleteOfficeAsync(filter);

        _collectionMock.Verify(x => x.DeleteOneAsync(filter, It.IsAny<CancellationToken>()), Times.Once);
    }
}