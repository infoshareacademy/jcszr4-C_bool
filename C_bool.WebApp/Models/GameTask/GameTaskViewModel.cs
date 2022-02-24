using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_bool.BLL.Enums;
using C_bool.WebApp.Models.Place;

namespace C_bool.WebApp.Models.GameTask
{
    public class GameTaskViewModel
    {
        public int Id { get; set; }
        public int PlaceId { get; set; }
        public PlaceViewModel Place { get; set; }
        public double DistanceFromUser { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string AfterDoneMessage { get; set; }
        public string Photo { get; set; }
        public TaskType Type { get; set; }
        public int Points { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidThru { get; set; }
        public bool IsActive { get; set; }
        public int CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public bool IsDoneLimited { get; set; }
        public int LeftDoneAttempts { get; set; }
        public bool IsUserFavorite { get; set; }
        public bool IsUserCompleted { get; set; }
        public bool IsOverdue { get; set; }
        public bool IsUserCreated { get; set; }
    }
}
