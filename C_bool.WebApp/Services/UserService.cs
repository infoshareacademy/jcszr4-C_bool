using System.Collections.Generic;
using C_bool.BLL.Models;
using C_bool.BLL.Models.User;
using C_bool.BLL.Repositories;

namespace C_bool.WebApp.Services
{
    public class UserService
    {
        private UsersRepository _usersRepository;

        public UserService()
        {
            _usersRepository = new UsersRepository();
            _usersRepository.AddFileDataToRepository();
        }

        public List<User> GetAllUsersFromFile() => _usersRepository.GetAll();

        public User GetUserById(string id) => _usersRepository.SearchById(id);
    }
}
