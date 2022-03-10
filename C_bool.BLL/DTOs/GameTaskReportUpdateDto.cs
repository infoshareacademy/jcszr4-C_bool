using C_bool.BLL.Enums;

namespace C_bool.BLL.DTOs
{
    public class GameTaskReportUpdateDto : IEntityReportDto
    {
        public string GameTaskName { get; set; }
        public TaskType Type { get; set; }
        public int Points { get; set; }
        public bool IsActive { get; set; }
        public bool IsDoneLimited { get; set; }
    }
}