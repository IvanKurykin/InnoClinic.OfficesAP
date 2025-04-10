using DAL.Entities;
using DAL.Interfaces;
using MongoDB.Driver;

namespace DAL.Repositories;

public class OfficeRepository(IMongoCollection<Office> offices) : IOfficeRepository
{
    public async Task<Office> CreateOfficeAsync(Office office, CancellationToken cancellationToken = default)
    {
        await offices.InsertOneAsync(office, cancellationToken: cancellationToken);
        return office;
    }

    public async Task<List<Office>> GetOfficesAsync(FilterDefinition<Office> filter, CancellationToken cancellationToken = default) =>
        await offices.Find(filter).ToListAsync(cancellationToken);

    public async Task<Office?> GetOfficeByIdAsync(FilterDefinition<Office> filter, CancellationToken cancellationToken = default) =>
        await offices.Find(filter).FirstOrDefaultAsync(cancellationToken);

    public async Task<Office> UpdateOfficeAsync(FilterDefinition<Office> filter, Office office, CancellationToken cancellationToken = default)
    {
        await offices.ReplaceOneAsync(filter, office, cancellationToken: cancellationToken);
        return office;
    }

    public async Task DeleteOfficeAsync(FilterDefinition<Office> filter, CancellationToken cancellationToken = default) =>
        await offices.DeleteOneAsync(filter, cancellationToken);
}
