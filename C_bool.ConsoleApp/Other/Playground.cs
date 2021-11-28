using System;
using System.Collections.Generic;
using C_bool.BLL.Logic;
using C_bool.BLL.Models.Places;
using C_bool.BLL.Repositories;
using C_bool.ConsoleApp.ConsoleHelpers;

namespace C_bool.ConsoleApp.Other
{
    public class Playground
    {
        public static void TestAddToRepositoryAndPrintInfo()
        {
            var placesRepository = new PlacesRepository();
            placesRepository.AddFileDataToRepository();

            //z Googla
            //placesRepository.AddApiDataToRepository("bank", 50.26855256464339, 19.02468223856004,100000,"point_of_interest");

            var usersRepository = new UsersRepository();
            usersRepository.AddFileDataToRepository();

            //do testów - przypisanie losowej ilości punktów do użytkowników
            var random = new Random();
            foreach (var user in usersRepository.Repository)
            {
                user.Points = random.Next(0, 1000);
            }

            //wyświetla listę wszystkich użytkowników
            GetInfo.UserInformation(usersRepository.Repository, "");

            //wyświetla listę wszystkich miejsc
            GetInfo.PlaceInformation(placesRepository.Repository, "");


            //szukanie miejsc w pobliżu wskazanej lokalizacji
            var radius = 200;
            var latitude = 50.26855256464339;
            var longitude = 19.02468223856004;

            //szukanie miejsc po szerokości i długości
            var nearbyPlaces = SearchNearbyPlaces.GetPlaces(placesRepository.Repository, latitude, longitude, radius);
            Console.WriteLine($"\n\n\nMIEJSCA W POBLIŻU {latitude},{longitude} w promieniu {radius}m:");
            GetInfo.PlaceInformation(nearbyPlaces, "");

            //szukanie miejsc w List<Places> po nazwie i miejsc w pobliżu
            var searchString = "Bistro Pizza";
            radius = 300;
            var placeToSearchFrom = placesRepository.Repository.Find(place => place.Name.Contains(searchString));
            nearbyPlaces = SearchNearbyPlaces.GetPlaces(placesRepository.Repository, placeToSearchFrom, radius);
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
            var usersByPoints = SearchUsers.ByPointsRange(usersRepository.Repository, minPoints, maxPoints, sortDescending);
            Console.WriteLine($"\n\n\nUŻYTKOWNICY Z MIN {minPoints} i MAX {maxPoints} ILOŚCIĄ PUNKTÓW, SORTOWANE {(sortDescending ? "MALEJĄCO":"ROSNĄCO")}:");
            GetInfo.UserInformation(usersByPoints, "");

            //szukanie miejsc w List<Places> po kategorii
            var searchCategory = "food";
            //var placeToSearchFrom = placesRepository.Repository.Find(place => place.Name.Contains(searchString));
            var placesWithMatchedCategories = SearchPlaceByCategory.GetPlaces(placesRepository.Repository, searchCategory);
            Console.WriteLine($"\n\n\nMIEJSCA W KATEGORII {searchCategory}:");
            GetInfo.PlaceInformation(placesWithMatchedCategories, "");

            var searchSubCategory = "meal_delivery";
            var placesWithMatchedSubCategories = SearchPlaceByCategory.GetPlacesExactCategory(placesWithMatchedCategories, searchSubCategory);
            Console.WriteLine($"\n\n\nUŚCIŚLONE O {searchSubCategory}:");
            GetInfo.PlaceInformation(placesWithMatchedSubCategories, "");
        }

        /// <summary>
        /// Assigns random points to users in repository
        /// </summary>
        /// <param name="repository"></param>
        public static void AssignRandomPointsToUsers(UsersRepository repository)
        {
            var random = new Random();
            foreach (var user in repository.Repository)
            {
                user.Points = random.Next(0, 1000);
            }
        }

        public static void GetFromKeyword()
        {
            var placesRepository = new PlacesRepository();
            List<Place> getPlace = GoogleAPI.ApiSearchPlaces("AIzaSyD4SpU-L5LIBsWSYVvp3GVn51gz2Dts8BY", "bankomat w pruszczu gdańskim", out TODO, out TODO);
            GetInfo.PlaceInformation(getPlace, "");
        }
    }
}
