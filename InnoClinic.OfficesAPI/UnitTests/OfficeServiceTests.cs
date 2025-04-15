using System.Text;
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

namespace UnitTests
{
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
        public async Task CreateOfficeAsyncReturnsOfficeResultDto()
        {
            var dto = new OfficeRequestDto
            {
                City = TestConstants.TestCity,
                Street = TestConstants.TestStreet,
                HouseNumber = TestConstants.TestHouseNumber,
                OfficeNumber = TestConstants.TestOfficeNumber,
                Photo = CreateTestFormFile()
            };

            var office = new Office();
            var createdOffice = new Office { Id = TestConstants.TestOfficeId };
            var expectedResult = new OfficeResultDto();

            _mapperMock.Setup(m => m.Map<Office>(dto)).Returns(office);
            _officeRepositoryMock.Setup(r => r.CreateOfficeAsync(office, It.IsAny<CancellationToken>())).ReturnsAsync(createdOffice);
            _mapperMock.Setup(m => m.Map<OfficeResultDto>(createdOffice)).Returns(expectedResult);
            _photoRepositoryMock.Setup(r => r.UploadPhotoAsync(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<CancellationToken>())).ReturnsAsync(TestConstants.TestPhotoFileId);

            var result = await _officeService.CreateOfficeAsync(dto);

            result.Should().Be(expectedResult);
        }

        [Fact]
        public async Task GetOfficesAsyncReturnsListOfOfficeResultDto()
        {
            var offices = new List<Office>
            {
                new Office { Id = TestConstants.TestOfficeId },
                new Office { Id = ObjectId.GenerateNewId() }
            };

            var expectedResults = offices.Select(o => new OfficeResultDto()).ToList();

            _officeRepositoryMock.Setup(r => r.GetOfficesAsync(It.IsAny<FilterDefinition<Office>>(), It.IsAny<CancellationToken>())).ReturnsAsync(offices);

            for (int i = 0; i < offices.Count; i++)
            {
                _mapperMock.Setup(m => m.Map<OfficeResultDto>(offices[i])).Returns(expectedResults[i]);
            }

            var result = await _officeService.GetOfficesAsync();

            result.Should().Equal(expectedResults);
        }

        [Fact]
        public async Task GetOfficeByIdAsyncReturnsOfficeResultDto()
        {
            var id = TestConstants.TestOfficeId.ToString();
            var office = new Office { Id = TestConstants.TestOfficeId };
            var expectedResult = new OfficeResultDto();

            _officeRepositoryMock.Setup(r => r.GetOfficeByIdAsync(It.IsAny<FilterDefinition<Office>>(), It.IsAny<CancellationToken>())).ReturnsAsync(office);
            _mapperMock.Setup(m => m.Map<OfficeResultDto>(office)).Returns(expectedResult);

            var result = await _officeService.GetOfficeByIdAsync(id);

            result.Should().Be(expectedResult);
        }

        [Fact]
        public async Task UpdateOfficeAsyncReturnsUpdatedOffice()
        {
            var id = TestConstants.TestOfficeId.ToString();
            var dto = new OfficeRequestDto
            {
                City = "Updated City",
                Photo = CreateTestFormFile()
            };

            var existingOffice = new Office { Id = TestConstants.TestOfficeId, PhotoFileId = TestConstants.TestPhotoFileId };
            var updatedOffice = new Office { Id = TestConstants.TestOfficeId };
            var expectedResult = new OfficeResultDto();

            _officeRepositoryMock.Setup(r => r.GetOfficeByIdAsync(It.IsAny<FilterDefinition<Office>>(), It.IsAny<CancellationToken>())).ReturnsAsync(existingOffice);
            _officeRepositoryMock.Setup(r => r.UpdateOfficeAsync(It.IsAny<FilterDefinition<Office>>(), It.IsAny<Office>(), It.IsAny<CancellationToken>())).ReturnsAsync(updatedOffice);
            _mapperMock.Setup(m => m.Map<OfficeResultDto>(updatedOffice)).Returns(expectedResult);
            _photoRepositoryMock.Setup(r => r.DeletePhotoAsync(It.IsAny<ObjectId>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            _photoRepositoryMock.Setup(r => r.UploadPhotoAsync(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<CancellationToken>())).ReturnsAsync(TestConstants.TestPhotoFileId);

            var result = await _officeService.UpdateOfficeAsync(id, dto);

            result.Should().Be(expectedResult);
            _photoRepositoryMock.Verify(r => r.DeletePhotoAsync(TestConstants.TestPhotoFileId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateOfficeStatusAsyncReturnsOfficeWithUpdatedStatus()
        {
            var id = TestConstants.TestOfficeId.ToString();
            var isActive = false;
            var office = new Office { Id = TestConstants.TestOfficeId, IsActive = true };
            var updatedOffice = new Office { Id = TestConstants.TestOfficeId, IsActive = isActive };
            var expectedResult = new OfficeResultDto { IsActive = isActive };

            _officeRepositoryMock.Setup(r => r.GetOfficeByIdAsync(It.IsAny<FilterDefinition<Office>>(), It.IsAny<CancellationToken>())).ReturnsAsync(office);
            _officeRepositoryMock.Setup(r => r.UpdateOfficeAsync(It.IsAny<FilterDefinition<Office>>(), It.IsAny<Office>(), It.IsAny<CancellationToken>())).ReturnsAsync(updatedOffice);
            _mapperMock.Setup(m => m.Map<OfficeResultDto>(updatedOffice)).Returns(expectedResult);

            var result = await _officeService.UpdateOfficeStatusAsync(id, isActive);

            result.Should().Be(expectedResult);
            result.IsActive.Should().Be(isActive);
        }

        [Fact]
        public async Task DeleteOfficeAsyncDeletesOfficeAndPhoto()
        {
            var id = TestConstants.TestOfficeId.ToString();
            var office = new Office { Id = TestConstants.TestOfficeId, PhotoFileId = TestConstants.TestPhotoFileId };

            _officeRepositoryMock.Setup(r => r.GetOfficeByIdAsync(It.IsAny<FilterDefinition<Office>>(), It.IsAny<CancellationToken>())).ReturnsAsync(office);
            _officeRepositoryMock.Setup(r => r.DeleteOfficeAsync(It.IsAny<FilterDefinition<Office>>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
            _photoRepositoryMock.Setup(r => r.DeletePhotoAsync(It.IsAny<ObjectId>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            await _officeService.DeleteOfficeAsync(id);

            _photoRepositoryMock.Verify(r => r.DeletePhotoAsync(TestConstants.TestPhotoFileId, It.IsAny<CancellationToken>()), Times.Once);
            _officeRepositoryMock.Verify(r => r.DeleteOfficeAsync(It.IsAny<FilterDefinition<Office>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        private IFormFile CreateTestFormFile()
        {
            var content = "Test file content";
            var fileName = TestConstants.TestPhotoFileName;
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

            return new FormFile(stream, 0, stream.Length, "id_from_form", fileName);
        }
    }
}