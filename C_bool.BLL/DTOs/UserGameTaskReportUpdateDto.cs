using System;

namespace C_bool.BLL.DTOs
{
    public class UserGameTaskReportUpdateDto : IEntityReportDto
    {
        public bool IsDone { get; set; }
        public DateTime DoneOn { get; set; }
    }
}