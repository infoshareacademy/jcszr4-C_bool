using System;
using C_bool.BLL.Enums;

namespace C_bool.BLL.DTOs
{
    public class GameTaskReportCreateDto : IEntityReportDto
    {
        public int GameTaskId { get; set; }
        public string GameTaskName { get; set; }
        public int PlaceId { get; set; }
        public TaskType GameTaskType { get; set; }
        public int Points { get; set; }
        public bool IsActive { get; set; }
        public int CreatedById { get; set; }
        public bool IsDoneLimited { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}