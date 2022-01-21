using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Models.GooglePlaces;
using C_bool.WebApp.Models.Place;

namespace C_bool.WebApp.Models.Profiles
{
    public class PlaceProfile : Profile
    {
        public PlaceProfile()
        {
            CreateMap<BLL.DAL.Entities.Place, PlaceViewModel>()
                .ForMember(dest => dest.ActiveTaskCount, o => o.MapFrom(src => src.Tasks.Count));
            CreateMap<BLL.DAL.Entities.Place, PlaceEditModel>();
            CreateMap<PlaceViewModel, PlaceMapModel>()
                .ForMember(dest => dest.Name, o => o.MapFrom(src => src.Name.Replace("\"", "")))
                .ForMember(dest => dest.Address, o => o.MapFrom(src => src.Address.Replace("\"", "")))
                .ForMember(dest => dest.ShortDescription, o => o.MapFrom(src => src.ShortDescription.Replace("\"", "")));
            CreateMap<PlaceEditModel, BLL.DAL.Entities.Place>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<GooglePlace, BLL.DAL.Entities.Place>()
                .ForMember(dest => dest.Id, o => o.Ignore())
                .ForMember(dest => dest.GoogleId, o => o.MapFrom(src=> src.Id))
                .ForMember(dest => dest.Latitude, o => o.MapFrom(src=> src.Geometry.Location.Latitude))
                .ForMember(dest => dest.Longitude, o => o.MapFrom(src=> src.Geometry.Location.Longitude))
                .ForMember(dest => dest.ShortDescription, o => o.NullSubstitute("?"))
                .ForMember(dest => dest.Description, o => o.NullSubstitute("?"))
                .ForMember(dest => dest.Photo, o => o.Ignore())
                .ForMember(dest => dest.IsUserCreated, o => o.NullSubstitute(false));
        }
    }
}