namespace C_bool.BLL.DTOs
{
    public class PlaceReportUpdateDto : IEntityReportDto
    {
        public string PlaceName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string[] Types { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
    }
}