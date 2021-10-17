using AutoMapper;
using MicroServiceExamplePlatformService.Dtos;
using MicroServiceExamplePlatformService.Models;
using PlatformService;

namespace MicroServiceExamplePlatformService.Profiles
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            //Source -> Target
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformCreateDto, Platform>();
            CreateMap<PlatformReadDto, PlatformPublishDto>();
            CreateMap<Platform, GrpcPlatformModel>()
                .ForMember(
                    dest
                        => dest.PlatformId, 
                    opt
                        => opt.MapFrom(src => src.Id));
        }
    }
}