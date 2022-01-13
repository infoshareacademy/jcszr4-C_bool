using System;
using System.Collections.Generic;
using C_bool.BLL.Enums;
using Microsoft.AspNetCore.Identity;


namespace C_bool.BLL.DAL.Entities
{
    public class User : IdentityUser<int>, IEntity
    {
        public override int Id { get; set; }
        public Gender Gender { get; set; }

        //TODO: banowanie?
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Points { get; set; }
        public virtual List<UserPlace> FavPlaces { get; set; }

        //TODO: sprawdzić - czy IQueryable to łyknie??? :-)
        public virtual List<UserGameTask> UserGameTasks { get; set; }

        public User()
        {
            IsActive = true;
            CreatedOn = DateTime.UtcNow;
            FavPlaces = new List<UserPlace>();
            UserGameTasks = new List<UserGameTask>();
        }
    }
}
