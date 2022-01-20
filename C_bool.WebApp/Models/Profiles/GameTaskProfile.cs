using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GameTaskViewModel = C_bool.WebApp.Models.GameTask.GameTaskViewModel;

namespace C_bool.WebApp.Models.Profiles
{
    public class GameTaskProfile :Profile
    {
        public GameTaskProfile()
        {
            CreateMap<BLL.DAL.Entities.GameTask, GameTaskViewModel>()
                .ForMember(dest => dest.PlaceId, o => o.MapFrom(src => src.Place.Id))
                .ForMember(dest => dest.ShortDescription, o => o.NullSubstitute("?"))
                .ForMember(dest => dest.Description, o => o.NullSubstitute("?"))
                .ForMember(dest => dest.Type, o => o.MapFrom(src => src.Type))
                .ForMember(dest => dest.TextCriterion, o => o.NullSubstitute("?"))
                .ReverseMap();
        }
    }
}
