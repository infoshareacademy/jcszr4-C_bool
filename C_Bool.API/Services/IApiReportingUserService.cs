using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_Bool.API.DAL.Entities;

namespace C_Bool.API.Services
{
     public interface IApiReportingUserService
    {
        public IEnumerable<User> GetUser();
        public void Add(User user);
        public int NumberOfActiveUsers();
    }
}
