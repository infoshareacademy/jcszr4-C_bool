namespace C_bool.WebApp.Models
{
    public class SearchUsersModel
    {
        public string Name { get; }
        public string Email { get; }
        public string Gender { get; }
        public string IsActive { get; }
        public string IsDescending { get; }

        public SearchUsersModel(string name, string email, string gender, string isActive, string isDescending)
        {
            Name = name;
            Email = email;
            Gender = gender;
            IsActive = isActive;
            IsDescending = isDescending;

        }
    }
}