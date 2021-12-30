using System;

namespace C_bool.BLL.DAL.Entities
{
    public class Entity : IEntity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
