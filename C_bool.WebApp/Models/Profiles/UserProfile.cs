using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using C_bool.BLL.DAL.Entities;
using C_bool.WebApp.Models.Place;
using C_bool.WebApp.Models.User;

namespace C_bool.WebApp.Models.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<BLL.DAL.Entities.User, UserViewModel>();
        }
    }
}