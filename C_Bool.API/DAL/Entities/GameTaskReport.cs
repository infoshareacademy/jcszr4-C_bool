using System;
using System.Collections.Generic;
using C_Bool.API.Enums;

namespace C_Bool.API.DAL.Entities
{
    public class GameTaskReport : Entity
    {
        public int GameTaskId { get; set; }
        public string GameTaskName { get; set; }
        public int PlaceId { get; set; }
        public TaskType Type { get; set; }
        public int Points { get; set; }
        public bool IsActive { get; set; }
        public int CreatedById { get; set; }
        public bool IsDoneLimited { get; set; }
    }
}