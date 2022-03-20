using System;
using C_bool.BLL.Enums;

namespace C_bool.BLL.DTOs
{
    public class UserGameTaskReportCreateDto : IEntityReportDto
    {
        public int UserGameTaskId { get; set; }
        public int UserId { get; set; }
        public int GameTaskId { get; set; }
        public TaskType GameTaskType { get; set; }
        public int PlaceId { get; set; }
        public bool IsDone { get; set; }
        public DateTime DoneOn { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}