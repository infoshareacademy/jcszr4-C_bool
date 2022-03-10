using System;
using C_Bool.API.Enums;

namespace C_Bool.API.DTOs
{
    public class UserGameTaskReportCreateDto
    {
        public int UserId { get; set; }
        public int GameTaskId { get; set; }
        public TaskType GameTaskType { get; set; }
        public int PlaceId { get; set; }
        public bool IsDone { get; set; }
        public DateTime DoneOn { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}