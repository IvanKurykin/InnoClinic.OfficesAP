using AutoMapper;
using BLL.DTO;
using DAL.Entities;

namespace BLL.Mapper;

public class OfficeProfile : Profile
{
    public OfficeProfile()
    {
        CreateMap<OfficeForCreatingDto, Office>()
           .ForMember(dest => dest.Id, opt => opt.Ignore())
           .ForMember(dest => dest.PhotoFileId, opt => opt.Ignore())
           .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
           .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
           .ForMember(dest => dest.HouseNumber, opt => opt.MapFrom(src => src.HouseNumber))
           .ForMember(dest => dest.OfficeNumber, opt => opt.MapFrom(src => src.OfficeNumber))
           .ForMember(dest => dest.RegistryPhoneNumber, opt => opt.MapFrom(src => src.RegistryPhoneNumber))
           .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));

        CreateMap<OfficeForUpdatingDto, Office>()
           .ForMember(dest => dest.Id, opt => opt.Ignore())
           .ForMember(dest => dest.PhotoFileId, opt => opt.Ignore())
           .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
           .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
           .ForMember(dest => dest.HouseNumber, opt => opt.MapFrom(src => src.HouseNumber))
           .ForMember(dest => dest.OfficeNumber, opt => opt.MapFrom(src => src.OfficeNumber))
           .ForMember(dest => dest.RegistryPhoneNumber, opt => opt.MapFrom(src => src.RegistryPhoneNumber))
           .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));

        CreateMap<Office, OfficeDto>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Photo, opt => opt.Ignore()) 
           .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.City}, {src.Street}, {src.HouseNumber}" + (string.IsNullOrEmpty(src.OfficeNumber) ? "" : $", {src.OfficeNumber}")));
    }
}

