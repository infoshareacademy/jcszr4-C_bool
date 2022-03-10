using AutoMapper;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.DTOs;

namespace C_bool.WebApp.Models.Profiles
{
    public class ReportServiceProfile : Profile
    {
        public ReportServiceProfile()
        {
            CreateMap<UserGameTask, UserGameTaskReportCreateDto>()
                .ForMember(dest => dest.GameTaskType,
                    opts =>
                        opts.MapFrom(src => src.GameTask.Type))
                .ForMember(dest => dest.PlaceId,
                    opts =>
                        opts.MapFrom(src => src.GameTask.Place.Id));

            CreateMap<UserGameTask, UserGameTaskReportUpdateDto>();
        }
    }
}