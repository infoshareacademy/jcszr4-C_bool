
using System;

namespace C_Bool.API.DAL.Entities
{
    public interface IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
