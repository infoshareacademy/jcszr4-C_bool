using System;
using System.Collections.Generic;
using System.IO;
using C_bool.BLL.Models.Users;
using Newtonsoft.Json;

namespace C_bool.BLL.Repositories
{
    public class UsersRepository : IRepository
    {
        public List<User> Users { get; private set; }

        public UsersRepository()
        {
            Users = new List<User>();
        }

        public void AddFileDataToRepository()
        {
            try
            {
                var jsonStream = new StreamReader("users.json").BaseStream;
                var streamReader = new StreamReader(jsonStream);
                var repository = JsonConvert.DeserializeObject<List<User>>(streamReader.ReadToEnd());

                if (repository == null)
                {
                    Console.WriteLine("Plik jest pusty!");
                }
                else
                {
                    Users = repository;
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Nie znaleziono pliku: " + ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Błąd dostępu do pliku: " + ex.Message);
            }
        }
    }
}