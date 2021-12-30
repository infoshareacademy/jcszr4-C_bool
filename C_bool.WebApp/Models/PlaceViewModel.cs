namespace C_bool.WebApp.Models
{
    public class PlaceViewModel
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Address { get; set; }
        public int ActiveTaskCount { get; set; }
    }
}
