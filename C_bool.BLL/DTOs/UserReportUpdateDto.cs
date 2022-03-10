namespace C_bool.BLL.DTOs
{
    public class UserReportUpdateDto : IEntityReportDto
    {
        public string UserName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsActive { get; set; }
    }
}