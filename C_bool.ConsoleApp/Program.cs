using System;
using System.Collections.Generic;
using C_bool.BLL.Places;
using C_bool.BLL.Repositories;
using C_bool.BLL.Users;

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

            User.PrintInformation(usersRepository.Users, "");

            Place.PrintInformation(placesRepository.Places, "");

        }
    }
}