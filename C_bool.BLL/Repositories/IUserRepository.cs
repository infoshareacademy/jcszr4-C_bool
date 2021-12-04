using System.Collections.Generic;
using C_bool.BLL.Models;
using C_bool.BLL.Models.User;

namespace C_bool.BLL.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        
        
        List<User> SearchByFirstName(List<User> users, string firstName);
        List<User> SearchByLastName(List<User> users, string lastName);
        List<User> SearchByEmail(List<User> users, string email);
        List<User> SearchByGender(List<User> users, Gender gender);
        List<User> SearchActive(List<User> users);
        List<User> OrderByPoints(List<User> users, bool isDescending);
        void AddFileDataToRepository();
        void AddUser(User user);

    }
}