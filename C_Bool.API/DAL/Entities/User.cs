using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;

namespace C_Bool.API.DAL.Entities
{
    public class User : Entity
    {
        public int UserId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsActive { get; set; }

    }
}
