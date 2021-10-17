using AutoMapper;
using MicroServiceExampleCommandService.Dtos;
using MicroServiceExampleCommandService.Models;
using PlatformService;

namespace MicroServiceExampleCommandService.Profiles
{
    public class CommandProfiles : Profile
    {
        public CommandProfiles()
        {
            // Source ---> Target
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<Command, CommandReadDto>();
            CreateMap<PlatformPublishedDto, Platform>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            
            CreateMap<GrpcPlatformModel, Platform>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.PlatformId));
        }
    }
}