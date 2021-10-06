using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_bool.BLL.Users
{
    public class User
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "isActive")]
        public bool IsActive { get; set; }

        [JsonProperty(PropertyName = "age")]
        public int Age { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "company")]
        public string Company { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "registered")]
        public string Registered { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public double Longitude { get; set; }

        public int Points { get; set; }

        static public void PrintInformation(List<User> list, string Id)
        {
            foreach (var user in list)
            {
                var outputString = $"\t| Imię: {user.FirstName}\n\t| Nazwisko: {user.LastName}\n\t| Płeć: {user.Gender}\n\t| Wiek: {user.Age}\n\t| Adres: {user.Address}\n\t| E-mail: {user.Email}\n\t| Telefon: {user.Phone}\n\t| Firma: {user.Company}\n\t------------\n\t| Aktywny: {user.IsActive}\n\t| Szer. geo.: {user.Latitude}\n\t| Wys. geo.: {user.Longitude}\n\t| Punkty: {user.Points}\n";

                if (user.Id.Equals(Id))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"ID: {user.Id}");
                    Console.ResetColor();
                    Console.Write(outputString);
                    return;
                }
                else if (Id.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"ID: {user.Id}");
                    Console.ResetColor();
                    Console.Write(outputString);
                }
            }
        }
    }
}
