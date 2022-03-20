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
                .ForMember(dest => dest.UserGameTaskId,
                    opts =>
                        opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.GameTaskType,
                    opts =>
                        opts.MapFrom(src => src.GameTask.Type))
                .ForMember(dest => dest.PlaceId,
                    opts =>
                        opts.MapFrom(src => src.GameTask.Place.Id));

            CreateMap<UserGameTask, UserGameTaskReportUpdateDto>();

            CreateMap<BLL.DAL.Entities.GameTask, GameTaskReportCreateDto>()
                .ForMember(dest => dest.GameTaskId,
                    opts =>
                        opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.GameTaskName,
                    opts =>
                        opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.PlaceId,
                    opts =>
                        opts.MapFrom(src => src.Place.Id))
                .ForMember(dest => dest.GameTaskType,
                    opts =>
                        opts.MapFrom(src => src.Type));

            CreateMap<BLL.DAL.Entities.GameTask, GameTaskReportUpdateDto>()
                .ForMember(dest => dest.GameTaskName,
                    opts =>
                        opts.MapFrom(src => src.Name));

            CreateMap<BLL.DAL.Entities.Place, PlaceReportCreateDto>()
                .ForMember(dest => dest.PlaceId,
                    opts =>
                        opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.PlaceName,
                    opts =>
                        opts.MapFrom(src => src.Name));

            CreateMap<BLL.DAL.Entities.Place, PlaceReportUpdateDto>()
                .ForMember(dest => dest.PlaceName,
                    opts =>
                        opts.MapFrom(src => src.Name));

            CreateMap<BLL.DAL.Entities.User, UserReportCreateDto>()
                .ForMember(dest => dest.UserId,
                    opts =>
                        opts.MapFrom(src => src.Id));

            CreateMap<BLL.DAL.Entities.User, UserReportUpdateDto>();
        }
    }
}