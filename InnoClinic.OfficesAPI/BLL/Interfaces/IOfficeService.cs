using BLL.DTO;
using MongoDB.Bson;

namespace BLL.Interfaces;

public interface IOfficeService
{
    Task<OfficeDto> CreateOfficeAsync(OfficeForCreatingDto dto, CancellationToken cancellationToken = default);
    Task<List<OfficeDto>> GetOfficesAsync(CancellationToken cancellationToken = default);
    Task<OfficeDto> GetOfficeByIdAsync(ObjectId id, CancellationToken cancellationToken = default);
    Task<OfficeDto> UpdateOfficeAsync(ObjectId id, OfficeForUpdatingDto dto, CancellationToken cancellationToken = default);
    Task<OfficeDto> UpdateOfficeStatusAsync(ObjectId id, OfficeForChangingStatusDto dto, CancellationToken cancellationToken = default);
    Task DeleteOfficeAsync(ObjectId id, CancellationToken cancellationToken = default);
}