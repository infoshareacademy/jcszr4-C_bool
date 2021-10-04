using System;
using System.Drawing;

namespace C_bool.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var placesStream = BLL.JsonUtils.ReadStream("Files/places.json");
            var usersStream = BLL.JsonUtils.ReadStream("Files/users.json");

            var places = BLL.JsonUtils.GetPlacesFromJson(placesStream);
            var users = BLL.JsonUtils.GetUsersFromJson(usersStream);

            BLL.JsonUtils.PrintPlacesInformation(places, "");
            BLL.JsonUtils.PrintUsersInformation(users, ""); //jeśli puste wyświetla wszystkie
        }
    }
}
