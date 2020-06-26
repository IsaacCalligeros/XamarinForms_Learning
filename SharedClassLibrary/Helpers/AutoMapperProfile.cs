using AutoMapper;
using SharedClassLibrary.Models;
using SharedClassLibrary.Dtos;
using System.Linq;

namespace SharedClassLibrary.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForDetailedDto>()
                .ForMember(dest => dest.Age, opt => {
                    opt.MapFrom(d => d.DateofBirth.CalculateAge());
                });
            CreateMap<UserForDetailedDto, User>();
            CreateMap<User, UserForSignedInDto>()
                .ForMember(dest => dest.Age, opt => {
                    opt.MapFrom(d => d.DateofBirth.CalculateAge());
                });
            CreateMap<UserForRegisterDto, User>();
            CreateMap<Photo, PhotosForDetailedDto>();
            CreateMap<Photo, PhotosForReturnDto>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<Location, Location>();
            CreateMap<TaskTemplate, TaskTemplate>();
            CreateMap<LocationUser, LocationUser>();
            CreateMap<LocationUser, LocationUserForReturn>();
        }
    }
}