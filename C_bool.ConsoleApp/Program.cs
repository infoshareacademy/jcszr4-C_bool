using System;
using System.Collections.Generic;
using C_bool.BLL.Places;
using C_bool.BLL.Repositories;
using C_bool.BLL.Users;
using C_bool.BLL.Logic;

namespace C_bool.ConsoleApp
{
    class Program
    {

        static void Main(string[] args)
        {
            var placesRepository = new PlacesRepository();
            placesRepository.AddFileDataToRepository();

            var usersRepository = new UsersRepository();
            usersRepository.AddFileDataToRepository();

           // User.PrintInformation(usersRepository.Users, "");

           // Place.PrintInformation(placesRepository.Places, "");

            double radius = 200;

            List<Place> nearbyPlaces = SearchNearbyPlaces.GetPlaces(50.26855256464339, 19.02468223856004, radius, placesRepository.Places);
            Console.WriteLine("MIEJSCA W POBLIŻU:");
            Place.PrintInformation(nearbyPlaces, "");

            double radius = 1500;

            List<Place> nearbyPlaces = SearchNearbyPlaces.GetPlaces(50.26855256464339, 19.02468223856004, radius, placesRepository.Places);
            Console.WriteLine("MIEJSCA W POBLIŻU:");
            Place.PrintInformation(nearbyPlaces, "");

        }
        
    }
}