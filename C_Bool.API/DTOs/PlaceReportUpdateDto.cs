namespace C_Bool.API.DTOs
{
    public class PlaceReportUpdateDto
    {
        public string PlaceName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string[] Types { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
    }
}