using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using C_Bool.API.Enums;

namespace C_Bool.API.DAL.Entities
{
    public class UserGameTaskReport : Entity
    {
        public int UserId { get; set; }
        public int GameTaskId { get; set; }
        public TaskType GameTaskType { get; set; }
        public int PlaceId { get; set; }
        public bool IsDone { get; set; }
        public DateTime DoneOn { get; set; }
    }
}