using DAL.Entities;
using MongoDB.Driver;

namespace DAL.Interfaces;

public interface IOfficeRepository
{
    Task<Office> CreateOfficeAsync(Office office, CancellationToken cancellationToken = default);
    Task<List<Office>> GetOfficesAsync(FilterDefinition<Office> filter, CancellationToken cancellationToken = default);
    Task<Office?> GetOfficeByIdAsync(FilterDefinition<Office> filter, CancellationToken cancellationToken = default);
    Task<Office> UpdateOfficeAsync(FilterDefinition<Office> filter, Office office, CancellationToken cancellationToken = default);
    Task DeleteOfficeAsync(FilterDefinition<Office> filter, CancellationToken cancellationToken = default);
}