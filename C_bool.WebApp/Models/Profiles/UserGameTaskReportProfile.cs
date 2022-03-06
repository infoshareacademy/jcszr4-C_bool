using AutoMapper;
using C_bool.BLL.DAL.Entities;

namespace C_bool.WebApp.Models.Profiles
{
    public class UserGameTaskReportProfile : Profile
    {
        public UserGameTaskReportProfile()
        {
            CreateMap<UserGameTask, UserGameTaskReportProfile>();
        }
    }
}