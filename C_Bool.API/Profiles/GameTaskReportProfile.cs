using AutoMapper;
using C_Bool.API.DAL.Entities;
using C_Bool.API.DTOs;

namespace C_Bool.API.Profiles
{
    public class GameTaskReportProfile : Profile
    {
        public GameTaskReportProfile()
        {
            CreateMap<GameTaskReportCreateDto, GameTaskReport>();

            CreateMap<GameTaskReportUpdateDto, GameTaskReport>()
                .ForAllMembers(opts => opts.Condition(
                    (src, dest, srcMember) => srcMember != null)
                );
        }
    }
}