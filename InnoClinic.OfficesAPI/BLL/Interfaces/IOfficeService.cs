using BLL.DTO;

namespace BLL.Interfaces;

public interface IOfficeService
{
    Task<OfficeResultDto> CreateOfficeAsync(OfficeRequestDto dto, CancellationToken cancellationToken = default);
    Task<List<OfficeResultDto>> GetOfficesAsync(CancellationToken cancellationToken = default);
    Task<OfficeResultDto> GetOfficeByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<OfficeResultDto> UpdateOfficeAsync(string id, OfficeRequestDto dto, CancellationToken cancellationToken = default);
    Task<OfficeResultDto> UpdateOfficeStatusAsync(string id, bool isActive, CancellationToken cancellationToken = default);
    Task DeleteOfficeAsync(string id, CancellationToken cancellationToken = default);
}