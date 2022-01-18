using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using C_bool.BLL.Models.GameTask;
using GameTaskViewModel = C_bool.WebApp.Models.GameTask.GameTaskViewModel;

namespace C_bool.WebApp.Models.Profiles
{
    public class GameTaskProfile :Profile
    {
        public GameTaskProfile()
        {
            CreateMap<BLL.DAL.Entities.GameTask, GameTaskViewModel>()
                .ForMember(dest => dest.PlaceId, o => o.MapFrom(src => src.Place.Id))
                .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Name))
                .ForMember(dest => dest.ShortDescription, o => o.MapFrom(src => src.ShortDescription))
                .ForMember(dest => dest.Description, o => o.NullSubstitute("?"))
                .ForMember(dest => dest.Photo, o => o.Ignore())
                .ForMember(dest => dest.Type, o => o.MapFrom(src => src.Type))
                .ForMember(dest => dest.TextCriterion, o => o.NullSubstitute("?"));
        }
    }
}
