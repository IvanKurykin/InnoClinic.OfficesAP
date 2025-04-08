using DAL.Entities;
using MongoDB.Bson;

namespace DAL.Interfaces;

public interface IOfficeRepository
{
    Task<Office> CreateOfficeAsync(Office office, CancellationToken cancellationToken = default);
    Task<List<Office>> GetOfficesAsync(CancellationToken cancellationToken = default);
    Task<Office?> GetOfficeByIdAsync(ObjectId id, CancellationToken cancellationToken = default);
    Task<Office> UpdateOfficeAsync(Office office, CancellationToken cancellationToken = default);
    Task DeleteOfficeAsync(Office office, CancellationToken cancellationToken = default);
}