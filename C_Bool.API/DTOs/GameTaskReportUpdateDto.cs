using C_Bool.API.Enums;

namespace C_Bool.API.DTOs
{
    public class GameTaskReportUpdateDto
    {
        public string GameTaskName { get; set; }
        public int Points { get; set; }
        public bool IsActive { get; set; }
        public bool IsDoneLimited { get; set; }
    }
}