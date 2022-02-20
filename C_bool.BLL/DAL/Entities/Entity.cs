using System;

namespace C_bool.BLL.DAL.Entities
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
