using System;

namespace C_bool.BLL.DTOs
{
    public class PlaceReportCreateDto : IEntityReportDto
    {
        public int PlaceId { get; set; }
        public string PlaceName { get; set; }
        public string GoogleId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string[] Types { get; set; }
        public string Address { get; set; }
        public bool IsUserCreated { get; set; }
        public int CreatedById { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}