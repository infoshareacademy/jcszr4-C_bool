using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_bool.API.Repositories;
using C_Bool.API.DAL.Entities;

namespace C_Bool.API.Services
{
    public class ApiReportingUserService : IApiReportingUserService
    {
        private readonly IRepository<User> _userRepository;

        public ApiReportingUserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetUser()
        {
            return _userRepository.GetAll();
        }

        public void Add(User user)
        {
            _userRepository.Add(user);
        }

        public int NumberOfActiveUsers()
        {
            var numberOfActiveUsers = _userRepository.GetAll().Count(x => x.IsActive == true);
            return numberOfActiveUsers;
        }
    }
}