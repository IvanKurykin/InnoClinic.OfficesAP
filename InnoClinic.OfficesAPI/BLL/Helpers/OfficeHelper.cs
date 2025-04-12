using AutoMapper;
using BLL.DTO;
using BLL.Exceptions;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BLL.Helpers;

public static class OfficeHelper
{  
    public static async Task<Office> GetOfficeOrThrowAsync(IOfficeRepository officeRepository, ObjectId id, CancellationToken cancellationToken = default)
    {
        var office = await officeRepository.GetOfficeByIdAsync(Builders<Office>.Filter.Eq(o => o.Id, id), cancellationToken);
        if (office is null) throw new OfficeNotFoundException();
        return office;
    }

    public static async Task ProcessOfficePhotoAsync(IPhotoRepository photoRepository, Office office, IFormFile? newPhoto, ObjectId? oldPhotoId, CancellationToken cancellationToken = default)
    {
        if (newPhoto is null) throw new OfficePhotoException();

        if (oldPhotoId.HasValue) await photoRepository.DeletePhotoAsync(oldPhotoId.Value, cancellationToken);

        await ProcessAndUploadOfficePhotoAsync(photoRepository, office, newPhoto, cancellationToken);
    }

    public static async Task<OfficeDto> MapToDtoWithPhotoAsync(IPhotoRepository photoRepository, IMapper mapper, Office office, CancellationToken cancellationToken = default)
    {
        var dto = mapper.Map<OfficeDto>(office);

        if (office.PhotoFileId.HasValue) dto.Photo = await photoRepository.GetPhotoByIdAsync(office.PhotoFileId.Value, cancellationToken);

        return dto;
    }

    public static async Task<List<OfficeDto>> MapOfficesToDtoAsync(IPhotoRepository photoRepository, IMapper mapper, IEnumerable<Office> offices, CancellationToken cancellationToken = default)
    {
        if (offices is null) return new List<OfficeDto>();

        var dtoTasks = offices.Select(o => MapToDtoWithPhotoAsync(photoRepository, mapper, o, cancellationToken));
        return (await Task.WhenAll(dtoTasks)).ToList();
    }

    public static async Task ProcessAndUploadOfficePhotoAsync(IPhotoRepository photoRepository, Office office, IFormFile? photo, CancellationToken cancellationToken = default)
    {
        if (photo is null) throw new OfficePhotoException();

        using var memoryStream = new MemoryStream();
        await photo.CopyToAsync(memoryStream, cancellationToken);
        office.PhotoFileId = await photoRepository.UploadPhotoAsync(photo.FileName, memoryStream.ToArray(), cancellationToken);
    }
}