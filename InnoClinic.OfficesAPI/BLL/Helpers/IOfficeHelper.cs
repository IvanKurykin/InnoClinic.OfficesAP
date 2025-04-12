using BLL.DTO;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;

namespace BLL.Helpers;

public interface IOfficeHelper
{
    Task<Office> GetOfficeOrThrowAsync(ObjectId id, CancellationToken cancellationToken = default);
    Task ProcessOfficePhotoAsync(Office office, IFormFile? newPhoto, ObjectId? oldPhotoId, CancellationToken cancellationToken = default);
    Task<OfficeDto> MapToDtoWithPhotoAsync(Office office, CancellationToken cancellationToken = default);
    Task<List<OfficeDto>> MapOfficerToDtoAsync(IEnumerable<Office> offices, CancellationToken cancellationToken = default);
    Task ProcessAndUploadOfficePhotoAsync(Office office, IFormFile? photo, CancellationToken cancellationToken = default);
}
