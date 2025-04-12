using System.Reflection;
using DAL.Repositories;
using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using Moq;

namespace UnitTests.Repositories;

public class PhotoRepositoryTests
{
    private readonly Mock<IGridFSBucket> _bucketMock;
    private readonly PhotoRepository _photoRepository;

    public PhotoRepositoryTests()
    {
        _bucketMock = new Mock<IGridFSBucket>();
        _photoRepository = CreateMockedPhotoRepository();
    }

    private PhotoRepository CreateMockedPhotoRepository()
    {
        var clientMock = new Mock<IMongoClient>();
        var databaseMock = new Mock<IMongoDatabase>();
        var settingsMock = new Mock<MongoDatabaseSettings>();

        clientMock.Setup(c => c.Settings).Returns(new MongoClientSettings());
        databaseMock.Setup(d => d.Client).Returns(clientMock.Object);
        databaseMock.Setup(d => d.DatabaseNamespace).Returns(new DatabaseNamespace("test"));
        databaseMock.Setup(d => d.Settings).Returns(settingsMock.Object);

        var repository = new PhotoRepository(databaseMock.Object);
        var bucketField = typeof(PhotoRepository).GetField("bucket", BindingFlags.NonPublic | BindingFlags.Instance);

        bucketField?.SetValue(repository, _bucketMock.Object);

        return repository;
    }

    [Fact]
    public async Task UploadPhotoAsyncShouldReturnObjectId()
    {
        var fileName = "test.jpg";
        var content = new byte[] { 1, 2, 3 };
        var expectedId = ObjectId.GenerateNewId();

        _bucketMock.Setup(b => b.UploadFromBytesAsync(fileName, content, It.IsAny<GridFSUploadOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedId);

        var result = await _photoRepository.UploadPhotoAsync(fileName, content);

        result.Should().Be(expectedId);
    }

    [Fact]
    public async Task GetPhotoByIdAsyncShouldReturnPhotoContent()
    {
        var photoId = ObjectId.GenerateNewId();
        var expectedContent = new byte[] { 1, 2, 3 };

        _bucketMock.Setup(b => b.DownloadAsBytesAsync(photoId, It.IsAny<GridFSDownloadOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedContent);

        var result = await _photoRepository.GetPhotoByIdAsync(photoId);

        result.Should().Equal(expectedContent);
    }

    [Fact]
    public async Task DeletePhotoAsyncShouldCallDelete()
    {
        var photoId = ObjectId.GenerateNewId();

        _bucketMock.Setup(b => b.DeleteAsync(photoId, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        await _photoRepository.DeletePhotoAsync(photoId);

        _bucketMock.Verify(b => b.DeleteAsync(photoId, It.IsAny<CancellationToken>()), Times.Once);
    }
}