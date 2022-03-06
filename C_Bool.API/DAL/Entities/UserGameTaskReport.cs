using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace C_Bool.API.DAL.Entities
{
    public class UserGameTaskReport : Entity
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int GameTaskId { get; set; }
        public string GameTaskName { get; set; }
        public string GameTaskType { get; set; }
        public int PlaceId { get; set; }
        public string PlaceName { get; set; }
        public bool IsDone { get; set; }
        public DateTime DoneOn { get; set; }
    }
}