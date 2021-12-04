using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace C_bool.BLL.Models.User
{
    public class User : IEntity
    {
        public string Id { get; set; }
        [Display(Name = "First Name")]
        [Range(2, 30, ErrorMessage = "First name is too short or too long")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Range(2, 30, ErrorMessage = "Last name is too short or too long")]
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        [JsonProperty(PropertyName = "registered")]
        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }
        public int Points { get; set; }
    }
}
