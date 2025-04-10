using AutoMapper;
using BLL.DTO;
using BLL.Exceptions;
using BLL.Helpers;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BLL.Services;

public class OfficeService(IOfficeRepository officeRepository, IPhotoRepository photoRepository, IMapper mapper, OfficeHelper officeHelper) : IOfficeService
{
    public async Task<OfficeDto> CreateOfficeAsync(OfficeForCreatingDto dto, CancellationToken cancellationToken = default)
    {
        var office = mapper.Map<Office>(dto);

        if (dto.Photo is not null) await officeHelper.ProcessAndUploadOfficePhotoAsync(office, dto.Photo, cancellationToken);

        var createdOffice = await officeRepository.CreateOfficeAsync(office, cancellationToken);

        return await officeHelper.MapToDtoWithPhotoAsync(createdOffice, cancellationToken);
    }

    public async Task<List<OfficeDto>> GetOfficesAsync(CancellationToken cancellationToken = default)
    {
        var offices = await officeRepository.GetOfficesAsync(FilterDefinition<Office>.Empty, cancellationToken);
        return await officeHelper.MapOfficerToDtoAsync(offices, cancellationToken);
    }

    public async Task<OfficeDto> GetOfficeByIdAsync(ObjectId id, CancellationToken cancellationToken = default)
    {
        var office = await officeRepository.GetOfficeByIdAsync(Builders<Office>.Filter.Eq(o => o.Id, id), cancellationToken);

        if (office is null) throw new OfficeNotFoundException();

        return await officeHelper.MapToDtoWithPhotoAsync(office, cancellationToken);
    }

    public async Task<OfficeDto> UpdateOfficeAsync(ObjectId id, OfficeForUpdatingDto dto, CancellationToken cancellationToken = default)
    {
        var office = await officeRepository.GetOfficeByIdAsync(Builders<Office>.Filter.Eq(o => o.Id, id), cancellationToken);

        if (office is null) throw new OfficeNotFoundException();

        var oldPhotoFileId = office.PhotoFileId;

        mapper.Map(dto, office);

        await officeHelper.ProcessOfficePhotoAsync(office, dto.Photo, oldPhotoFileId, cancellationToken);

        var updatedOffice = await officeRepository.UpdateOfficeAsync(Builders<Office>.Filter.Eq(o => o.Id, id), office, cancellationToken);

        return await officeHelper.MapToDtoWithPhotoAsync(updatedOffice, cancellationToken);
    }

    public async Task<OfficeDto> UpdateOfficeStatusAsync(ObjectId id, OfficeForChangingStatusDto dto, CancellationToken cancellationToken = default)
    {
        var office = await officeRepository.GetOfficeByIdAsync(Builders<Office>.Filter.Eq(o => o.Id, id), cancellationToken);

        if (office is null) throw new OfficeNotFoundException();

        office.IsActive = dto.IsActive;

        var updatedOffice = await officeRepository.UpdateOfficeAsync(Builders<Office>.Filter.Eq(o => o.Id, id), office, cancellationToken);

        return mapper.Map<OfficeDto>(updatedOffice);
    }

    public async Task DeleteOfficeAsync(ObjectId id, CancellationToken cancellationToken = default)
    {
        var office = await officeRepository.GetOfficeByIdAsync(Builders<Office>.Filter.Eq(o => o.Id, id),cancellationToken);

        if (office is null) throw new OfficeNotFoundException();

        if (office.PhotoFileId.HasValue) await photoRepository.DeletePhotoAsync(office.PhotoFileId.Value, cancellationToken);
        
        await officeRepository.DeleteOfficeAsync(Builders<Office>.Filter.Eq(o => o.Id, id), cancellationToken);
    }
}