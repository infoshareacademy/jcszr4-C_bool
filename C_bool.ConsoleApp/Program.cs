using System;
using C_bool.BLL.Repositories;

namespace C_bool.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var placesRepository = new PlacesRepository();
            placesRepository.AddFileDataToRepository();

            foreach (var place in placesRepository.Places)
            {
                Console.WriteLine();
                Console.WriteLine(
                    $"ID : {place.PlaceId},\nNAZWA: {place.Name},\nDLUGOSC GEOGRAFICZNA: {place.Geometry.Location.Latitude},\nSZEROKOSC GEOGRAFICZNA: {place.Geometry.Location.Latitude},\nSREDNIA OCENA: {place.Rating},\nILOSC OCEN: {place.UserRatingsTotal},\nTYPY: {String.Join(",", place.Types)}");
                Console.WriteLine();
            }
        }
    }
}