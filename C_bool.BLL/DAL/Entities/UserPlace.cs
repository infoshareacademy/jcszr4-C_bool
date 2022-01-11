using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C_bool.BLL.DAL.Entities
{
    public class UserPlace : Entity
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }
        [Key, Column(Order = 1)]
        public int PlaceId { get; set; }
        public virtual User User { get; set; }
        public virtual Place Place { get; set; }

        public UserPlace()
        {
            
        }

        public UserPlace(User user, Place place)
        {
            User = user;
            Place = place;
        }

    }
}