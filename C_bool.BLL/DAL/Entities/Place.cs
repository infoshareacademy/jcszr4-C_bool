using System.Collections.Generic;

namespace C_bool.BLL.DAL.Entities
{
    public class Place : Entity
    {
        public string GoogleId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string[] Types { get; set; }
        public double Rating { get; set; }
        public int UserRatingsTotal { get; set; }
        public string Address { get; set; }
        public bool IsUserCreated { get; set; }
        public string CreatedById { get; set; }
        public IList<GameTask> Tasks { get; set; }
        public virtual List<UserPlace> FavPlaces { get; set; }
        public bool IsActive { get; set; }
    }
}