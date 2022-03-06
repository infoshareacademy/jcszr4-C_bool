using AutoMapper;
using C_Bool.API.DAL.Entities;
using C_Bool.API.DTOs;

namespace C_Bool.API.Profiles
{
    public class UserGameTaskProfile : Profile
    {
        public UserGameTaskProfile()
        {
            CreateMap<UserGameTaskReport, UserGameTaskWriteDto>();
        }
    }
}