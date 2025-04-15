using DAL.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace DAL.Repositories;

public class PhotoRepository(IMongoDatabase database) : IPhotoRepository
{
    private readonly GridFSBucket bucket = new GridFSBucket(database);

    public async Task DeletePhotoAsync(ObjectId id, CancellationToken cancellationToken = default) =>
        await bucket.DeleteAsync(id, cancellationToken);

    public async Task<byte[]?> GetPhotoByIdAsync(ObjectId id, CancellationToken cancellationToken = default) =>
        await bucket.DownloadAsBytesAsync(id, cancellationToken: cancellationToken);

    public async Task<ObjectId> UploadPhotoAsync(string fileName, byte[] content, CancellationToken cancellationToken = default) =>
        await bucket.UploadFromBytesAsync(fileName, content, cancellationToken: cancellationToken);
}