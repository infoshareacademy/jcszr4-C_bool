using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C_bool.BLL.DAL.Entities
{
    public class Entity : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }

        public Entity()
        {
            CreatedOn = DateTime.UtcNow;
        }
    }
}
