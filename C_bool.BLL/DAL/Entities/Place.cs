using System.Collections.Generic;

//TODO: pobawić się automapperem
//TODO: co to jest IList i dlaczego
namespace C_bool.BLL.DAL.Entities
{
    public class Place : Entity
    {
        //public Geometry Geometry { get; set; }
        public string GoogleId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; } = "Brak opisu";
        public string Description { get; set; }
        public string Photo { get; set; }
        public string[] Types { get; set; }
        public double Rating { get; set; } = 0.0;

        public int UserRatingsTotal { get; set; } = 0;
        public string Address { get; set; } = "no_address";

        public bool IsUserCreated { get; set; }

        public IList<GameTask> Tasks { get; set; }
        public virtual List<UserPlace> FavPlaces { get; set; }
    }
}