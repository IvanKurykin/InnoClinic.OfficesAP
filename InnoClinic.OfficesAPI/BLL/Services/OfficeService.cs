﻿using AutoMapper;
using BLL.DTO;
using BLL.Exceptions;
using BLL.Helpers;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BLL.Services;

public class OfficeService(IOfficeRepository officeRepository, IPhotoRepository photoRepository, IMapper mapper) : IOfficeService
{
    public async Task<OfficeResultDto> CreateOfficeAsync(OfficeRequestDto dto, CancellationToken cancellationToken = default)
    {
        var office = mapper.Map<Office>(dto);

        if (dto.Photo is not null) await ProcessAndUploadOfficePhotoAsync(office, dto.Photo, cancellationToken);

        var createdOffice = await officeRepository.CreateOfficeAsync(office, cancellationToken);

        return await MapToDtoWithPhotoAsync(createdOffice, cancellationToken);
    }

    public async Task<List<OfficeResultDto>> GetOfficesAsync(CancellationToken cancellationToken = default)
    {
        var offices = await officeRepository.GetOfficesAsync(FilterDefinition<Office>.Empty, cancellationToken);
        return await MapOfficesToDtoAsync(offices, cancellationToken);
    }

    public async Task<OfficeResultDto> GetOfficeByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var objectId = ParseHelper.ParseId(id);

        var office = await officeRepository.GetOfficeByIdAsync(Builders<Office>.Filter.Eq(o => o.Id, objectId), cancellationToken);

        if (office is null) throw new OfficeNotFoundException();

        return await MapToDtoWithPhotoAsync(office, cancellationToken);
    }

    public async Task<OfficeResultDto> UpdateOfficeAsync(string id, OfficeRequestDto dto, CancellationToken cancellationToken = default)
    {
        var objectId = ParseHelper.ParseId(id);

        var office = await officeRepository.GetOfficeByIdAsync(Builders<Office>.Filter.Eq(o => o.Id, objectId), cancellationToken);

        if (office is null) throw new OfficeNotFoundException();

        var oldPhotoFileId = office.PhotoFileId;

        mapper.Map(dto, office);

        await ProcessOfficePhotoAsync(office, dto.Photo, oldPhotoFileId, cancellationToken);

        var updatedOffice = await officeRepository.UpdateOfficeAsync(Builders<Office>.Filter.Eq(o => o.Id, objectId), office, cancellationToken);

        return await MapToDtoWithPhotoAsync(updatedOffice, cancellationToken);
    }

    public async Task<OfficeResultDto> UpdateOfficeStatusAsync(string id, bool isActive, CancellationToken cancellationToken = default)
    {
        var objectId = ParseHelper.ParseId(id);

        var office = await officeRepository.GetOfficeByIdAsync(Builders<Office>.Filter.Eq(o => o.Id, objectId), cancellationToken);

        if (office is null) throw new OfficeNotFoundException();

        office.IsActive = isActive;

        var updatedOffice = await officeRepository.UpdateOfficeAsync(Builders<Office>.Filter.Eq(o => o.Id, objectId), office, cancellationToken);

        return mapper.Map<OfficeResultDto>(updatedOffice);
    }

    public async Task DeleteOfficeAsync(string id, CancellationToken cancellationToken = default)
    {
        var objectId = ParseHelper.ParseId(id);

        var office = await officeRepository.GetOfficeByIdAsync(Builders<Office>.Filter.Eq(o => o.Id, objectId),cancellationToken);

        if (office is null) throw new OfficeNotFoundException();

        if (office.PhotoFileId.HasValue) await photoRepository.DeletePhotoAsync(office.PhotoFileId.Value, cancellationToken);
        
        await officeRepository.DeleteOfficeAsync(Builders<Office>.Filter.Eq(o => o.Id, objectId), cancellationToken);
    }

    private async Task ProcessOfficePhotoAsync(Office office, IFormFile? newPhoto, ObjectId? oldPhotoId, CancellationToken cancellationToken = default)
    {
        if (newPhoto is null) throw new OfficePhotoException();

        if (oldPhotoId.HasValue) await photoRepository.DeletePhotoAsync(oldPhotoId.Value, cancellationToken);

        await ProcessAndUploadOfficePhotoAsync(office, newPhoto, cancellationToken);
    }

    private async Task<OfficeResultDto> MapToDtoWithPhotoAsync(Office office, CancellationToken cancellationToken = default)
    {
        var dto = mapper.Map<OfficeResultDto>(office);

        if (office.PhotoFileId.HasValue) dto.Photo = await photoRepository.GetPhotoByIdAsync(office.PhotoFileId.Value, cancellationToken);

        return dto;
    }

    private async Task<List<OfficeResultDto>> MapOfficesToDtoAsync(IEnumerable<Office> offices, CancellationToken cancellationToken = default)
    {
        if (offices is null) return new List<OfficeResultDto>();

        var dtoTasks = offices.Select(o => MapToDtoWithPhotoAsync(o, cancellationToken));

        return (await Task.WhenAll(dtoTasks)).ToList();
    }

    private async Task ProcessAndUploadOfficePhotoAsync(Office office, IFormFile? photo, CancellationToken cancellationToken = default)
    {
        if (photo is null) throw new OfficePhotoException();

        using var memoryStream = new MemoryStream();
        await photo.CopyToAsync(memoryStream, cancellationToken);
        office.PhotoFileId = await photoRepository.UploadPhotoAsync(photo.FileName, memoryStream.ToArray(), cancellationToken);
    }
}