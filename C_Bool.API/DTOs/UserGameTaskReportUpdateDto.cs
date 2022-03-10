using System;

namespace C_Bool.API.DTOs
{
    public class UserGameTaskReportUpdateDto
    {
        public bool IsDone { get; set; }
        public DateTime DoneOn { get; set; }
    }
}