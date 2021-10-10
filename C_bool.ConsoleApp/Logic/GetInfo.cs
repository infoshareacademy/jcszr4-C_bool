﻿using System;
using System.Collections.Generic;
using C_bool.BLL.Models.Places;
using C_bool.BLL.Models.Users;

namespace C_bool.ConsoleApp.Logic
{
    public class GetInfo
    {
        /// <summary>
        /// prints basic information about place based on PlaceId, PlaceId might be empty
        /// </summary>
        /// <param name="places">input List of Place objects</param>
        /// <param name="id">Id number of place, if empty - prints all places</param>
        public static void PlaceInformation(List<Place> places, string id)
        {
            foreach (var place in places)
            {
                var outputString = $"\t| Ocena: {place.Rating} (wszystkich ocen: {place.UserRatingsTotal})\n\t| Adres: {place.Address}\n\t| Szer. geo.: {place.Geometry.Location.Latitude}\n\t| Wys. geo.: {place.Geometry.Location.Longitude}\n";

                if (place.PlaceId.Equals(id))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"NAZWA: {place.Name}");
                    Console.ResetColor();
                    Console.Write(outputString);
                    return;
                }
                else if (id.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"NAZWA: {place.Name}");
                    Console.ResetColor();
                    Console.Write(outputString);
                }
            }
        }

        /// <summary>
        /// prints basic information about user based on Id, Id might be empty
        /// </summary>
        /// <param name="list">input List of User objects</param>
        /// <param name="id">Id number of user, if empy - prints all users</param>
        public static void UserInformation(List<User> list, string id)
        {
            foreach (var user in list)
            {
                var outputString = $"\t| Imię: {user.FirstName}\n\t| Nazwisko: {user.LastName}\n\t| Płeć: {user.Gender}\n\t| Wiek: {user.Age}\n\t| Adres: {user.Address}\n\t| E-mail: {user.Email}\n\t| Telefon: {user.Phone}\n\t| Firma: {user.Company}\n\t------------\n\t| Aktywny: {user.IsActive}\n\t| Szer. geo.: {user.Latitude}\n\t| Wys. geo.: {user.Longitude}\n\t| Punkty: {user.Points}\n";

                if (user.Id.Equals(id))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"ID: {user.Id}");
                    Console.ResetColor();
                    Console.Write(outputString);
                    return;
                }
                else if (id.Length == 0)
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
