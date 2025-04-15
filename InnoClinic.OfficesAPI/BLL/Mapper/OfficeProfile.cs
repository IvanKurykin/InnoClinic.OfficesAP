using AutoMapper;
using BLL.DTO;
using DAL.Entities;

namespace BLL.Mapper;

public class OfficeProfile : Profile
{
    public OfficeProfile()
    {
        CreateMap<OfficeRequestDto, Office>()
           .ForMember(dest => dest.Id, opt => opt.Ignore())
           .ForMember(dest => dest.PhotoFileId, opt => opt.Ignore());

        CreateMap<Office, OfficeResultDto>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
           .ForMember(dest => dest.Photo, opt => opt.Ignore()) 
           .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.City}, {src.Street}, {src.HouseNumber}" + (string.IsNullOrEmpty(src.OfficeNumber) ? "" : $", {src.OfficeNumber}")));
    }
}