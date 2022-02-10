using System.Collections.Generic;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Enums;

namespace C_bool.WebApp.Models.User
{
    public class UserViewModel
    {
        public  int Id { get; set; }
        public string UserName { get; set; }
        public Gender Gender { get; set; }
        public string Photo { get; set; }
        public int Points { get; set; }
        public virtual List<UserPlace> FavPlaces { get; set; }
        public virtual List<UserGameTask> UserGameTasks { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
