namespace C_Bool.API.DTOs
{
    public class UserReportUpdateDto
    {
        public string UserName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsActive { get; set; }
    }
}