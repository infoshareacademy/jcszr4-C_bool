namespace C_bool.BLL.Models
{
    public class User : IEntity
    {
        public string Id { get; set; }
        public bool IsActive { get; set; }
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Registered { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Points { get; set; }


    }
}
