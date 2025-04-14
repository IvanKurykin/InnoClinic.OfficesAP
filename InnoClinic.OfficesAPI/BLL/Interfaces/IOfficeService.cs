using BLL.DTO;
using MongoDB.Bson;

namespace BLL.Interfaces;

public interface IOfficeService
{
    Task<OfficeDto> CreateOfficeAsync(OfficeForCreatingDto dto, CancellationToken cancellationToken = default);
    Task<List<OfficeDto>> GetOfficesAsync(CancellationToken cancellationToken = default);
    Task<OfficeDto> GetOfficeByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<OfficeDto> UpdateOfficeAsync(string id, OfficeForUpdatingDto dto, CancellationToken cancellationToken = default);
    Task<OfficeDto> UpdateOfficeStatusAsync(string id, OfficeForChangingStatusDto dto, CancellationToken cancellationToken = default);
    Task DeleteOfficeAsync(string id, CancellationToken cancellationToken = default);
}