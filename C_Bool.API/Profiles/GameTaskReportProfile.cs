using System.Collections.Generic;
using AutoMapper;
using C_Bool.API.DAL.Entities;
using C_Bool.API.DTOs;

namespace C_Bool.API.Profiles
{
    public class GameTaskReportProfile : Profile
    {
        public GameTaskReportProfile()
        {
            CreateMap<GameTaskReportCreateDto, GameTaskReport>()
                .ForMember(
                    dest => dest.Type,
                    opts =>
                        opts.MapFrom(src => src.GameTaskType)
                );
            CreateMap<GameTaskReportUpdateDto, GameTaskReport>()
                .ForAllMembers(opts => opts.Condition(
                    (src, dest, srcMember) => srcMember != null)
                );
            CreateMap<KeyValuePair<string, int>, GetCountByDto>()
                .ForMember(
                    dest => dest.Name,
                    opts =>
                        opts.MapFrom(src => src.Key)
                )
                .ForMember(
                    dest => dest.Count,
                    opts =>
                        opts.MapFrom(src => src.Value)
                );
        }
    }
}