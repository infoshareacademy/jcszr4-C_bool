using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using C_bool.WebApp.Models.GameTask;

namespace C_bool.WebApp.Models.Profiles
{
    public class GameTaskProfile :Profile
    {
        public GameTaskProfile()
        {
            CreateMap<BLL.DAL.Entities.GameTask, GameTaskViewModel>();
            CreateMap<BLL.DAL.Entities.UserGameTask, UserGameTaskViewModel>();
            CreateMap<GameTaskEditModel, BLL.DAL.Entities.GameTask>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<BLL.DAL.Entities.GameTask, GameTaskEditModel>()
                .ForMember(dest => dest.PlaceId, o => o.MapFrom(src => src.Place.Id))
                .ForMember(dest => dest.Type, o => o.MapFrom(src => src.Type));
            CreateMap<GameTaskEditModel, BLL.DAL.Entities.GameTask>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
