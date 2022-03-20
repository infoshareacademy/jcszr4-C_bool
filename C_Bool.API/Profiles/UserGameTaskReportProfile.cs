using AutoMapper;
using C_Bool.API.DAL.Entities;
using C_Bool.API.DTOs;

namespace C_Bool.API.Profiles
{
    public class UserGameTaskReportProfile : Profile
    {
        public UserGameTaskReportProfile()
        {
            CreateMap<UserGameTaskReportCreateDto, UserGameTaskReport>();

            CreateMap<UserGameTaskReportUpdateDto, UserGameTaskReport>()
                .ForAllMembers(opts => opts.Condition(
                    (src, dest, srcMember) => srcMember != null)
                );
        }
    }
}