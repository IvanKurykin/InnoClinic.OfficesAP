using AutoMapper;
using BLL.DTO;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using UnitTests.TestCases;

namespace UnitTests;

public class OfficeServiceTests
{
    private readonly Mock<IOfficeRepository> _officeRepositoryMock;
    private readonly Mock<IPhotoRepository> _photoRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly OfficeService _officeService;

    public OfficeServiceTests()
    {
        _officeRepositoryMock = new Mock<IOfficeRepository>();
        _photoRepositoryMock = new Mock<IPhotoRepository>();
        _mapperMock = new Mock<IMapper>();
        _officeService = new OfficeService(_officeRepositoryMock.Object, _photoRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task CreateOfficeAsyncShouldCreateOfficeWhenValidData()
    {
        var dto = new OfficeForCreatingDto
        {
            City = TestConstants.TestCity,
            Street = TestConstants.TestStreet,
            HouseNumber = TestConstants.TestHouseNumber,
            OfficeNumber = TestConstants.TestOfficeNumber,
            RegistryPhoneNumber = TestConstants.TestRegistryPhoneNumber,
            IsActive = true,
            Photo = new Mock<IFormFile>().Object
        };

        var office = new Office();
        var createdOffice = new Office { Id = TestConstants.TestOfficeId };
        var expectedDto = new OfficeDto { Id = TestConstants.TestOfficeId };

        _mapperMock.Setup(x => x.Map<Office>(dto)).Returns(office);
        _officeRepositoryMock.Setup(x => x.CreateOfficeAsync(office, It.IsAny<CancellationToken>())).ReturnsAsync(createdOffice);
        _photoRepositoryMock.Setup(x => x.UploadPhotoAsync(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<CancellationToken>())).ReturnsAsync(ObjectId.GenerateNewId());
        _mapperMock.Setup(x => x.Map<OfficeDto>(createdOffice)).Returns(expectedDto);
        _photoRepositoryMock.Setup(x => x.GetPhotoByIdAsync(It.IsAny<ObjectId>(), It.IsAny<CancellationToken>())).ReturnsAsync(new byte[0]);

        var result = await _officeService.CreateOfficeAsync(dto);

        result.Should().NotBeNull();
        result.Id.Should().Be(TestConstants.TestOfficeId);
    }

    [Fact]
    public async Task GetOfficesAsyncShouldReturnAllOffices()
    {
        var offices = new List<Office> { new Office { Id = ObjectId.GenerateNewId() }, new Office { Id = ObjectId.GenerateNewId() } };

        var expectedDtos = offices.Select(o => new OfficeDto { Id = o.Id }).ToList();

        _officeRepositoryMock.Setup(x => x.GetOfficesAsync(FilterDefinition<Office>.Empty, It.IsAny<CancellationToken>())).ReturnsAsync(offices);
        _mapperMock.Setup(x => x.Map<OfficeDto>(It.IsAny<Office>())).Returns<Office>(o => new OfficeDto { Id = o.Id });
        _photoRepositoryMock.Setup(x => x.GetPhotoByIdAsync(It.IsAny<ObjectId>(), It.IsAny<CancellationToken>())).ReturnsAsync(new byte[0]);

        var result = await _officeService.GetOfficesAsync();

        result.Should().NotBeNull();
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetOfficeByIdAsyncShouldReturnOfficeWhenExists()
    {
        var office = new Office { Id = TestConstants.TestOfficeId };
        var expectedDto = new OfficeDto { Id = TestConstants.TestOfficeId };

        _officeRepositoryMock.Setup(x => x.GetOfficeByIdAsync(It.IsAny<FilterDefinition<Office>>(), It.IsAny<CancellationToken>())).ReturnsAsync(office);
        _mapperMock.Setup(x => x.Map<OfficeDto>(office)).Returns(expectedDto);
        _photoRepositoryMock.Setup(x => x.GetPhotoByIdAsync(It.IsAny<ObjectId>(), It.IsAny<CancellationToken>())).ReturnsAsync(new byte[0]);

        var result = await _officeService.GetOfficeByIdAsync(TestConstants.TestOfficeId);

        result.Should().NotBeNull();
        result.Id.Should().Be(TestConstants.TestOfficeId);
    }

    [Fact]
    public async Task UpdateOfficeAsyncShouldUpdateOfficeWhenValidData()
    {
        var dto = new OfficeForUpdatingDto { City = "Updated City", Photo = new Mock<IFormFile>().Object };
        var existingOffice = new Office { Id = TestConstants.TestOfficeId, PhotoFileId = TestConstants.TestPhotoFileId };
        var updatedOffice = new Office { Id = TestConstants.TestOfficeId };
        var expectedDto = new OfficeDto { Id = TestConstants.TestOfficeId };

        _officeRepositoryMock.Setup(x => x.GetOfficeByIdAsync(It.IsAny<FilterDefinition<Office>>(), It.IsAny<CancellationToken>())).ReturnsAsync(existingOffice);
        _officeRepositoryMock.Setup(x => x.UpdateOfficeAsync(It.IsAny<FilterDefinition<Office>>(), It.IsAny<Office>(), It.IsAny<CancellationToken>())).ReturnsAsync(updatedOffice);
        _mapperMock.Setup(x => x.Map<OfficeDto>(updatedOffice)).Returns(expectedDto);
        _photoRepositoryMock.Setup(x => x.GetPhotoByIdAsync(It.IsAny<ObjectId>(), It.IsAny<CancellationToken>())).ReturnsAsync(new byte[0]);

        var result = await _officeService.UpdateOfficeAsync(TestConstants.TestOfficeId, dto);

        _mapperMock.Verify(x => x.Map(dto, existingOffice), Times.Once);
    }

    [Fact]
    public async Task UpdateOfficeStatusAsyncShouldUpdateStatus()
    {
        var dto = new OfficeForChangingStatusDto { IsActive = false };
        var existingOffice = new Office { Id = TestConstants.TestOfficeId, IsActive = true };
        var updatedOffice = new Office { Id = TestConstants.TestOfficeId, IsActive = false };
        var expectedDto = new OfficeDto { Id = TestConstants.TestOfficeId, IsActive = false };

        _officeRepositoryMock.Setup(x => x.GetOfficeByIdAsync(It.IsAny<FilterDefinition<Office>>(), It.IsAny<CancellationToken>())).ReturnsAsync(existingOffice);
        _officeRepositoryMock.Setup(x => x.UpdateOfficeAsync(It.IsAny<FilterDefinition<Office>>(), It.IsAny<Office>(), It.IsAny<CancellationToken>())).ReturnsAsync(updatedOffice);
        _mapperMock.Setup(x => x.Map<OfficeDto>(updatedOffice)).Returns(expectedDto);

        var result = await _officeService.UpdateOfficeStatusAsync(TestConstants.TestOfficeId, dto);

        result.Should().NotBeNull();
        result.IsActive.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteOfficeAsyncShouldDeleteOfficeWhenExists()
    {
        var existingOffice = new Office
        {
            Id = TestConstants.TestOfficeId,
            PhotoFileId = TestConstants.TestPhotoFileId
        };

        _officeRepositoryMock.Setup(x => x.GetOfficeByIdAsync(It.IsAny<FilterDefinition<Office>>(), It.IsAny<CancellationToken>())).ReturnsAsync(existingOffice);

        await _officeService.DeleteOfficeAsync(TestConstants.TestOfficeId);

        _photoRepositoryMock.Verify(x => x.DeletePhotoAsync(TestConstants.TestPhotoFileId, It.IsAny<CancellationToken>()), Times.Once);
        _officeRepositoryMock.Verify(x => x.DeleteOfficeAsync(It.IsAny<FilterDefinition<Office>>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}