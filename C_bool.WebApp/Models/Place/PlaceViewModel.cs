using System.Collections.Generic;
using C_bool.WebApp.Models.GameTask;

namespace C_bool.WebApp.Models.Place
{
    public class PlaceViewModel
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double DistanceFromUser { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int ActiveTaskCount { get; set; }
        public IList<GameTaskViewModel> Tasks { get; set; }
        public string Photo { get; set; }
        public string[] Types { get; set; }
        public double Rating { get; set; }
        public int UserRatingsTotal { get; set; }
        public bool IsUserCreated { get; set; }
        public bool IsUserFavorite { get; set; }
        public string CreatedById { get; set; }

        public string CreatedOn { get; set; }
    }
}
