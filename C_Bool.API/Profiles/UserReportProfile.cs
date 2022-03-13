using AutoMapper;
using C_Bool.API.DAL.Entities;
using C_Bool.API.DTOs;

namespace C_Bool.API.Profiles
{
    public class UserReportProfile : Profile
    {
        public UserReportProfile()
        {
            CreateMap<UserReportCreateDto, UserReport>();

            CreateMap<UserReportUpdateDto, UserReport>()
                .ForAllMembers(opts => opts.Condition(
                    (src, dest, srcMember) => srcMember != null)
                );
        }
        
    }
}