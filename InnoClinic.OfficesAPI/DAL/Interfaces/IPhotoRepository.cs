using MongoDB.Bson;

namespace DAL.Interfaces;

public interface IPhotoRepository
{
    Task<ObjectId> UploadPhotoAsync(string fileName, byte[] content, CancellationToken cancellationToken = default);
    Task<byte[]?> GetPhotoByIdAsync(ObjectId id, CancellationToken cancellationToken = default);
    Task DeletePhotoAsync(ObjectId id, CancellationToken cancellationToken = default);
}