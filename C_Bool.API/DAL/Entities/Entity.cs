using System;

namespace C_Bool.API.DAL.Entities
{
    public class Entity : IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }

        public Entity()
        {
            CreatedOn = DateTime.UtcNow;
        }
    }
}
