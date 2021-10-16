using System;
using C_bool.BLL.Logic;
using C_bool.BLL.Repositories;
using C_bool.ConsoleApp.Logic;

namespace C_bool.ConsoleApp.Other
{
    public class Playground
    {
        public static void TestAddToRepositoryAndPrintInfo()
        {
            var placesRepository = new PlacesRepository();
            placesRepository.AddFileDataToRepository();

            //z Googla
            //placesRepository.AddFileDataToRepository("bank", 50.26855256464339, 19.02468223856004,100000,"point_of_interest");

            var usersRepository = new UsersRepository();
            usersRepository.AddFileDataToRepository();

            //do testów - przypisanie losowej ilości punktów do użytkowników
            var random = new Random();
            foreach (var user in usersRepository.Users)
            {
                user.Points = random.Next(0, 1000);
            }

            //wyświetla listę wszystkich użytkowników
            GetInfo.UserInformation(usersRepository.Users, "");

            //wyświetla listę wszystkich miejsc
            GetInfo.PlaceInformation(placesRepository.Places, "");


            //szukanie miejsc w pobliżu wskazanej lokalizacji
            var radius = 200;
            var latitude = 50.26855256464339;
            var longitude = 19.02468223856004;

            //szukanie miejsc po szerokości i długości
            var nearbyPlaces = SearchNearbyPlaces.GetPlaces(latitude, longitude, radius, placesRepository.Places);
            Console.WriteLine($"\n\n\nMIEJSCA W POBLIŻU {latitude},{longitude} w promieniu {radius}m:");
            GetInfo.PlaceInformation(nearbyPlaces, "");

            //szukanie miejsc w List<Places> po nazwie i miejsc w pobliżu
            var searchString = "Bistro Pizza";
            radius = 300;
            var placeToSearchFrom = placesRepository.Places.Find(place => place.Name.Contains(searchString));
            nearbyPlaces = SearchNearbyPlaces.GetPlaces(placeToSearchFrom, radius, placesRepository.Places);
            if (placeToSearchFrom != null)
            {
                Console.WriteLine(
                    $"\n\n\nMIEJSCA W POBLIŻU {placeToSearchFrom.Name} adres: {placeToSearchFrom.Address} w promieniu {radius}m:");
            }

            GetInfo.PlaceInformation(nearbyPlaces, "");

            //wyświetla listę użytkowników o określonej ilości punktów
            var minPoints = 100;
            var maxPoints = 500;
            var sortDescending = true;
            var usersByPoints = SearchUsers.ByPointsRange(usersRepository.Users, minPoints, maxPoints, sortDescending);
            Console.WriteLine($"\n\n\nUŻYTKOWNICY Z MIN {minPoints} i MAX {maxPoints} ILOŚCIĄ PUNKTÓW, SORTOWANE {(sortDescending ? "MALEJĄCO":"ROSNĄCO")}:");
            GetInfo.UserInformation(usersByPoints, "");
        }
    }
}
