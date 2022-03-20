using System;

namespace C_Bool.API.DTOs
{
    public class UserReportCreateDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}