using AutoMapper;
using C_Bool.API.DAL.Entities;
using C_Bool.API.DTOs;

namespace C_Bool.API.Profiles
{
    public class PlaceReportProfile : Profile
    {
        public PlaceReportProfile()
        {
            CreateMap<PlaceReportCreateDto, PlaceReport>();

            CreateMap<PlaceReportUpdateDto, PlaceReport>()
                .ForAllMembers(opts => opts.Condition(
                    (src, dest, srcMember) => srcMember != null)
                );
        }
    }
}